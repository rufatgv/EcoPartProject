using EcoPart.Web.UI.AppCode.Infrastructure;
using System.Collections.Generic;

namespace EcoPart.Web.UI.Models.Entities
{
    public class Category : BaseEntity
    {

        public string  Name { get; set; }

        public int? BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public int? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children { get; set; }
    }
}
