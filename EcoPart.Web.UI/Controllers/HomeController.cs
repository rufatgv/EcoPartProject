

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

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactPost model)
        {
            if (!ModelState.IsValid)
            {

                //return View(model);
                return Json(new
                {
                    error = true,
                    message = ModelState.SelectMany(ms => ms.Value.Errors).First().ErrorMessage

                });
            }

            await db.ContactPosts.AddAsync(model);
            await db.SaveChangesAsync();


            //return View();
            return Json(new
            {
                error = false,
                message = "We Will Contact You"

            });
        }


    }
}
