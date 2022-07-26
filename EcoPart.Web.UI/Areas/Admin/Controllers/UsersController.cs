using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities.Membership;
using EcoPart.Web.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly EcoPartsDbContext db;

        public UsersController(EcoPartsDbContext db)
        {
            this.db = db;
        }

        [Authorize("admin.users.index")]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 5)
        {
            var query = db.Users;
            var pagedModel = new PagedViewModel<EcoPartsUser>(query, pageIndex, pageSize);
            return View(pagedModel);
        }




        [Authorize("admin.users.details")]
        public async Task<IActionResult> Details(int id)
        {
            var user = await db.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (User == null)
            {
                return NotFound();
            }


            ViewBag.Roles = await (from r in db.Roles
                                   join ur in db.UserRoles
                                   on new { RoleId = r.Id, UserId = user.Id } equals new { ur.RoleId, ur.UserId } into lJoin
                                   from lj in lJoin.DefaultIfEmpty()
                                   select Tuple.Create(r.Id, r.Name, lj != null)).ToListAsync();

            ViewBag.principals = (from p in Program.principals
                                  join uc in db.UserClaims on new { ClaimValue = "1", ClaimType = p, UserId = user.Id } equals new { uc.ClaimValue, uc.ClaimType, uc.UserId } into lJoin
                                  from lj in lJoin.DefaultIfEmpty()
                                  select Tuple.Create(p, lj != null)).ToList();
            return View(user);
        }




        [Authorize("admin.users.setrole")]
        [HttpPost]
        [Route("/user-set-role")]
        public async Task<IActionResult> SetRole(int userId, int roleId, bool selected)
        {

            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var role = await db.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            if (user == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Xetali Sorgu"
                });
            }


            if (role == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Xetali Sorgu"
                });
            }


            if (selected)
            {
                if (await db.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId))
                {
                    return Json(new
                    {
                        error = true,
                        message = $"'{user.Name} {user.Surname}' adli istifadechi '{role.Name}' adli roldadir "
                    });
                }

                else
                {
                    db.UserRoles.Add(new EcoPartsUserRole
                    {
                        UserId = userId,
                        RoleId = roleId
                    });

                    await db.SaveChangesAsync();

                    return Json(new
                    {
                        error = false,
                        message = $"'{user.Name} {user.Surname}' adli istifadechi '{role.Name}' rola elave edildi "
                    });
                }
            }
            else
            {
                var userRole = await db.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

                if (userRole == null)
                {
                    return Json(new
                    {
                        error = true,
                        message = $"'{user.Name} {user.Surname}' adli istifadechi '{role.Name}'adli rolda deyil "
                    });
                }
                else
                {
                    db.UserRoles.Remove(userRole);

                    await db.SaveChangesAsync();

                    return Json(new
                    {
                        error = false,
                        message = $"'{user.Name} {user.Surname}' adli istifadechi '{role.Name}' roldan silindi "
                    });
                }
            }

        }
    }
}
