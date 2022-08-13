using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoPart.Web.UI.Controllers
{
    [AllowAnonymous]
    public class PreloaderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
