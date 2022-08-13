using EcoPart.Web.UI.Models.DataContexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EcoPart.Web.UI.Controllers
{
    [AllowAnonymous]
    public class FaqController : Controller
    {
        readonly EcoPartsDbContext db;
        public FaqController(EcoPartsDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var faqs = db.Faqs.ToList();
            return View(faqs);
        }
    }
}
