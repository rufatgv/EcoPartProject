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
            

            if (ModelState.IsValid)
            {
                var product = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            return View(command);
        }
        [Authorize(Policy = "admin.products.edit")]
        public async Task<IActionResult> Edit(ProductSingleQuery query)
        {
            var product = await mediator.Send(query);
            if (product == null)
            {
                return NotFound();
            }
            var command = new ProductEditCommand();
            command.Id = product.Id;
            command.ImagePath = product.ImagePath;
            command.Name = product.Name;
            command.CategoryId = product.CategoryId;
            command.ForSearch = product.ForSearch;
            command.Price = product.Price;
            command.PartCodeName = product.PartCodeName;
            ViewData["CategoryId"] = new SelectList(db.Categories.Where(s => s.DeletedById == null), "Id", "Name", product.CategoryId);
            //ViewBag.PartCodeNames = db.Products
            ViewBag.Codes = db.PartCodes.Where(ppc => ppc.DeletedById == null)
               .ToList();
            return View(command);
        }

        [HttpPost]
        [Authorize(Policy = "admin.products.edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, ProductEditCommand command)
        {

            if (id != command.Id)
            {
                return NotFound();
            }

            await mediator.Send(command);
            ViewData["CategoryId"] = new SelectList(db.Categories.Where(s => s.DeletedById == null), "Id", "Name", command.CategoryId);
            return RedirectToAction(nameof(Index));
        }

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
