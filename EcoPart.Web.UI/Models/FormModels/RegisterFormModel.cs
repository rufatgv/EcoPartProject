using System.ComponentModel.DataAnnotations;

namespace EcoPart.Web.UI.Models.FormModels
{
    public class RegisterFormModel
    {

        [Required]
        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Do not Match")]
        public string ConfirmPassword { get; set; }

    }
}
