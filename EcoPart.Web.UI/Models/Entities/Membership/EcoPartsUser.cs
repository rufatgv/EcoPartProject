using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EcoPart.Web.UI.Models.Entities.Membership
{
    public class EcoPartsUser : IdentityUser<int>
    {   
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

    }
}
