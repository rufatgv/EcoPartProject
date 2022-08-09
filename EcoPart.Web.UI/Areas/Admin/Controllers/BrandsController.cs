  using EcoPart.Web.UI.AppCode.Modules.BrandModule;
using EcoPart.Web.UI.Models.Entities;
using EcoPart.Web.UI.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {

        readonly IMediator mediator;

        public BrandsController(IMediator mediator)
        {

            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(int pageIndex = 1 , int pageSize = 5)
        {
            var data = await mediator.Send(new BrandsAllQuery());

            var pagedModel = new PagedViewModel<Brand>(data, pageIndex, pageSize);
            return View(pagedModel);
        }

        public async Task<IActionResult> Details(BrandSingleQuery query)
        {
            var entity = await mediator.Send(query);


            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BrandCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);

                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }


        public async Task<IActionResult> Edit(BrandSingleQuery query)
        {


            var entity = await mediator.Send(query);
            if (entity == null)
            {
                return NotFound();
            }

            var command = new BrandEdItCommand();
            command.Id = entity.Id;
            command.Name = entity.Name;
            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

      
        public async Task<IActionResult> Edit([FromRoute] int id, BrandEdItCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command);
        }


        [HttpPost]

       
        public async Task<IActionResult> Delete(BrandRemoveCommand command)
        {
            var response = await mediator.Send(command);

            return Json(response);

        }

    }
}
