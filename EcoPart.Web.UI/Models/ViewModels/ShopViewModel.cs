using EcoPart.Web.UI.Models.Entities;
using System.Collections.Generic;

namespace EcoPart.Web.UI.Models.ViewModels
{
    public class ShopViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public PagedViewModel<Product> PagedViewModel { get; set; }
    }
}
