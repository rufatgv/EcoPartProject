using EcoPart.Web.UI.AppCode.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace EcoPart.Web.UI.Models.Entities
{
    public class ContactPost : BaseEntity

    {
        [Required(ErrorMessage = "Insert Your Questions")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Insert Your Full Name")]
        public string FullName { get; set; }
        [EmailAddress(ErrorMessage ="E-mail is not valid")]
        [Required(ErrorMessage ="You should add E-mail")]
        public string Email { get; set; }

        public DateTime? AnswerDate { get; set; }

        public string AnswerMessage { get; set; }

        public int? AnsweredById { get; set; }
    }
}
