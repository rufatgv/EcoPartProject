using EcoPart.Web.UI.AppCode.Infrastructure;

namespace EcoPart.Web.UI.Models.Entities
{
    public class BlogPost : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
