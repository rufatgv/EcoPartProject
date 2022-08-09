using EcoPart.Web.UI.Models.DataContexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EcoPart.Web.UI.AppCode.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly EcoPartsDbContext db;

        public HeaderViewComponent(EcoPartsDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var entity = db.Categories
                .Where(c => c.DeletedById == null  )
                .ToList();

            ViewBag.Categories = entity;

            return View();
        }

    }
}
