using EcoPart.Web.UI.AppCode.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoPart.Web.UI.Models.Entities
{
    public class ProductPricing : HistoryEntity
    {
        public int TypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double Price { get; set; }
    }
}
