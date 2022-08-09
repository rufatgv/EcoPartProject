

using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly EcoPartsDbContext db;

        public HomeController(EcoPartsDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var entity = db.Categories
                .Where(c => c.DeletedById == null)
                .ToList();

            ViewBag.Categories = entity;

            return View();
        }

       
    }
}
