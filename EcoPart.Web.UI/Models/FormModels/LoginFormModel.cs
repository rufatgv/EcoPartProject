using System.ComponentModel.DataAnnotations;

namespace EcoPart.Web.UI.Models.FormModels
{
    public class LoginFormModel
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
