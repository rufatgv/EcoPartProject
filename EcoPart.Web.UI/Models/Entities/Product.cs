using EcoPart.Web.UI.AppCode.Infrastructure;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoPart.Web.UI.Models.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string PartCodeName { get; set; }
        public string ImagePath { get; set; }
        public string ForSearch { get; set; }

        [Column(TypeName="decimal(18,2)")] 
        public double Price { get; set; }

    }
}
