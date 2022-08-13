
using EcoPart.Web.UI.AppCode.Modules.BlogPostModule;
using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using EcoPart.Web.UI.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.Areas.Admin.Controllers
{
        [AllowAnonymous]
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        readonly EcoPartsDbContext db;
        readonly IMediator mediator;
        public BlogPostsController(EcoPartsDbContext db, IWebHostEnvironment env, IMediator mediator)
        {
            this.db = db;
            //this.env = env;
            this.mediator = mediator;
        }

        //[Authorize(Policy = "admin.blogposts.index")]
        public async Task<IActionResult> Index(BlogPostAllQuery query,int pageIndex = 1,int pageSize = 10)
        {
            var entity = await mediator.Send(query);
            var pagedModel = new PagedViewModel<BlogPost>(entity, pageIndex, pageSize);

            return View(pagedModel);

        }


        //[Authorize(Policy = "admin.blogposts.details")]
        public async Task<IActionResult> Details(BlogPostSingleQuery query)
        {
            var blog = await mediator.Send(query);

            if (blog == null)
            {
                return NotFound();
            }



            return View(blog);
        }

        //[Authorize(Policy = "admin.blogposts.create")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name");
            //ViewData["TagId"] = new SelectList(db.PostTags, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //[Authorize(Policy = "admin.blogposts.create")]
        public async Task<IActionResult> Create(BlogPostCreateCommand command)
        {
            var response = await mediator.Send(command);

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }


            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", command.CategoryId);
            //ViewData["TagId"] = new SelectList(db.PostTags, "Id", "Name");
            return View(command);
        }


        [Authorize(Policy = "admin.blogposts.edit")]
        public async Task<IActionResult> Edit(BlogPostSingleQuery query)
        {
            var blog = await mediator.Send(query);
            if(blog==null)
            {
                return NotFound();
            }

            var command = new BlogPostEditCommand();
            command.Id = blog.Id;
            command.Title = blog.Title;
            command.Paragraph = blog.Paragraph;
            command.ImagePath = blog.ImagePath;
            command.CategoryId = blog.CategoryId;
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", blog.CategoryId);

            return View(command);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //[Authorize(Policy = "admin.blogposts.edit")]
        public async Task<IActionResult> Edit([FromRoute] int id, BlogPostEditCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            await mediator.Send(command);
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", command.CategoryId);
            return RedirectToAction(nameof(Index));

        }


        //[Authorize(Policy = "admin.blogposts.delete")]
        public async Task<IActionResult> Delete(BlogPostRemoveCommand command)
        {
            var response = await mediator.Send(command);
            return Json(response);
        }



        

    }
}
