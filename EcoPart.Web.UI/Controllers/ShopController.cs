using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using EcoPart.Web.UI.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        readonly EcoPartsDbContext db;

        public ShopController(EcoPartsDbContext db)
        {
            this.db = db;
        }
        [Authorize(Policy = "shop.index")]
        public IActionResult Index(int pageIndex = 1, int pageSize = 5)
        {

            var model = new ShopViewModel();
            model.Categories = db.Categories
                .Where(b => b.DeletedById == null)
                .ToList();
            model.Brands = db.Brands
                .Where(pc => pc.DeletedById == null)
                .ToList();
            
            var query = db.Products.Include(p => p.Category)
                .ThenInclude(c=>c.Brand)
                .Where(b => b.DeletedById == null);
            model.PagedViewModel = new PagedViewModel<Product>(query, pageIndex, pageSize);
            return View(model);
        }
        [Authorize(Policy = "shop.category")]
        public IActionResult Category(int categoryId ,int pageIndex = 1, int pageSize = 5)
        {

            var model = new ShopViewModel();
            model.Categories = db.Categories
                .Where(b => b.DeletedById == null)
                .ToList();
            model.Brands = db.Brands
                .Where(pc => pc.DeletedById == null)
                .ToList();
            
            var query = db.Products.Include(p => p.Category).Where(b => b.DeletedById == null && b.CategoryId == categoryId);
            model.PagedViewModel = new PagedViewModel<Product>(query, pageIndex, pageSize);
            return View(model);
        }

        [Authorize(Policy = "shop.details")]
        public IActionResult Details(int id)
        {

            var model = new ShopViewModel();
            model.Product = db.Products
                .Include(p => p.Category)
                .ThenInclude(p => p.Brand)
                .FirstOrDefault(p => p.DeletedById == null && p.Id == id);

            return View(model);
        }

        [Authorize(Policy = "shop.search")]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction("Index", "Shop");
            }
            var model = new ShopViewModel();
            model.Products = await db.Products.Include(p => p.Category).Where(p => p.DeletedById == null && p.ForSearch.ToLower().Contains(query.ToLower())).ToListAsync();
        
            return View(model);
        }
        [Authorize(Policy = "shop.searchPartial")]
        public async Task<IActionResult> SearchPartial(string query)
        {
            var model = new ShopViewModel();
            model.Products = await db.Products.Where(p => p.DeletedById == null && p.ForSearch.ToLower().Contains(query.ToLower())).ToListAsync();
            return PartialView("_SearchPartialView", model);
        }


        public IActionResult Basket()
        {
            if (Request.Cookies.TryGetValue("cards", out string cards))
            {
                int[] idsFromCookie = cards.Split(",").Where(CheckIsNumber)
                        .Select(item => int.Parse(item))
                        .ToArray();

                var products = from p in db.Products.Where(p => p.DeletedById == null)
                               where idsFromCookie.Contains(p.Id) && p.DeletedById == null
                               select p;

                return View(products.ToList());

            }

            return View(new List<Product>());

        }

        private bool CheckIsNumber(string value)
        {
            return int.TryParse(value, out int v);
        }

        //public async Task<IActionResult> SearchInput(string key)
        //{
        //    List<Product> products = new List<Product>();
        //    if (key != null)
        //    {
        //        products = await db.Products
        //        .Where(p => p.Name.Contains(key)
        //        || p.Price.ToString().Contains(key)
        //        || p.Category.Name.Contains(key)
        //        || p.ForSearch.Contains(key))
        //        .ToListAsync();
        //    }
        //    return PartialView("_ProductListPartial", products);
        //}

    }
}
