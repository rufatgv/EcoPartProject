using EcoPart.Web.UI.AppCode.Infrastructure;

namespace EcoPart.Web.UI.Models.Entities
{
    public class Faq : BaseEntity
    {

        public string Question { get; set; }

        public string Answer { get; set; }
    }
}
