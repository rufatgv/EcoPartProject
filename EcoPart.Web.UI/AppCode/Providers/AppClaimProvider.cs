﻿using EcoPart.Web.UI.Models.DataContexts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Providers
{
    public class AppClaimProvider : IClaimsTransformation
    {
        private readonly EcoPartsDbContext db;

        public AppClaimProvider(EcoPartsDbContext db)
        {
            this.db = db;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {

            if (principal.Identity.IsAuthenticated && principal.Identity is ClaimsIdentity currentIdentity)
            {
                var userId = Convert.ToInt32(currentIdentity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
                var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);


                if (user! == null)
                {
                    currentIdentity.AddClaim(new Claim("name", user.Name));
                    currentIdentity.AddClaim(new Claim("surname", user.Surname));
                }

                var role = currentIdentity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role));
                while (role != null)
                {
                    currentIdentity.RemoveClaim(role);
                    role = currentIdentity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role));
                }

                #region Reload Roles for current user

                var currentRoles = (from ur in db.UserRoles
                                    join r in db.Roles on ur.RoleId equals r.Id
                                    where ur.UserId == userId
                                    select r.Name).ToArray();

                foreach (var roleName in currentRoles)
                {
                    currentIdentity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                }
                #endregion

                //Reload Claims for current user

                var currentClaims = currentIdentity.Claims.Where(c => Program.principals.Contains(c.Type))
                    .ToArray();


                foreach(var claim in currentClaims)
                {
                    currentIdentity.RemoveClaim(claim);
                }

                var currentPolicies = await (from uc in db.UserClaims
                                             where uc.UserId == userId
                                             && uc.ClaimValue == "1"
                                             select uc.ClaimType)
                  .Union(from rc in db.RoleClaims
                         join ur in db.UserRoles on rc.RoleId equals ur.RoleId
                         where ur.UserId == userId && rc.ClaimValue == "1"
                         select rc.ClaimType)
                  .ToListAsync();



                foreach (var policy in currentPolicies)
                {
                    currentIdentity.AddClaim(new Claim(policy,"1"));
                }

            }


            return principal;
        }
    }
}
