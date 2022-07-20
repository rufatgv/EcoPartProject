using Microsoft.AspNetCore.Mvc;

namespace EcoPart.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
