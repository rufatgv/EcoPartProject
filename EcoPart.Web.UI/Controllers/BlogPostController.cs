using EcoPart.Web.UI.Models.DataContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EcoPart.Web.UI.Controllers

{
    [AllowAnonymous]
    public class BlogPostController : Controller
    {

        readonly EcoPartsDbContext db;
        public BlogPostController(EcoPartsDbContext db)
        {
            this.db = db;
        }


        public IActionResult Index()
        {
            var data = db.BlogPosts
                .Where(bp => bp.DeletedById == null)
                .ToList();

            return View(data);
        }
    

        public IActionResult SinglePost()
        {
            return View();
        }
    }
}
