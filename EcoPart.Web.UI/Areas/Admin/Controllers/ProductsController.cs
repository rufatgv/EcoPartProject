using EcoPart.Web.UI.AppCode.Infrastructure;
using EcoPart.Web.UI.AppCode.Modules.ProductModule;
using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using EcoPart.Web.UI.Models.Entities.Membership;
using EcoPart.Web.UI.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class ProductsController : Controller
    {
        private readonly EcoPartsDbContext db;
        private readonly IMediator mediator;
        private readonly UserManager<EcoPartsUser> userManager;

        public ProductsController(EcoPartsDbContext db, IMediator mediator,
            UserManager<EcoPartsUser> userManager)
        {
            this.db = db;
            this.mediator = mediator;
            this.userManager = userManager;
        }
        [Authorize(Policy = "admin.products.index")]
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 5)
        {
            var query = db.Products
                .Include(p => p.Category)
                .Where(p => p.DeletedById == null);
            var pagedModel = new PagedViewModel<Product>(query, pageIndex, pageSize);
            return View(pagedModel);
        }
        [Authorize(Policy = "admin.products.details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products
                .Include(p => p.Category)
                .ThenInclude(c=>c.Brand)
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedById==null);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [Authorize(Policy = "admin.products.create")]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories.Where(c=>c.DeletedById==null), "Id", "Name");
            ViewBag.Codes = db.PartCodes.Where(ppc => ppc.DeletedById == null)
               .ToList();
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "admin.products.create")]
        public async Task<IActionResult> Create(ProductCreateCommand command)
        {
            var product = await mediator.Send(command);


            if (product?.ValidationResult != null && !product.ValidationResult.IsValid)
            {
                return Json(product.ValidationResult);
            }

            return Json(new CommandJsonResponse(false, $"Ugurlu emeliyyat, yeni mehsulun kodu:{product.Product.Id}"));
        }
        [Authorize(Policy = "admin.products.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await db.Products
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(db.Categories.Where(s => s.DeletedById == null), "Id", "Name", product.CategoryId);
            ViewBag.Codes = db.PartCodes.Where(ppc => ppc.DeletedById == null)
               .ToList();
            return View(product);
        }

        //[HttpPost]
        //[Authorize(Policy = "admin.products.edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(ProductEditCommand model)
        //{

        //    var product = await mediator.Send(model);

        //    if (product?.ValidationResult != null && !product.ValidationResult.IsValid)
        //    {
        //        return Json(product.ValidationResult);
        //    }

        //    return Json(new CommandJsonResponse(false, $"Ugurlu emeliyyat, yeni mehsulun kodu:{product.Product.Id}"));
        //}

        [HttpPost]
        [Authorize(Policy = "admin.products.delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = db.Products.FirstOrDefault(b => b.Id == id && b.DeletedById == null);
            if (entity == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Movcud deyil"
                });
            }
            var user = await userManager.GetUserAsync(User);
            entity.DeletedById = user.Id;
            entity.DeletedDate = DateTime.UtcNow.AddHours(4);
            db.SaveChanges();
            return Json(new
            {
                error = false,
                message = "Ugurla silindi"
            });
        }

    }
}
