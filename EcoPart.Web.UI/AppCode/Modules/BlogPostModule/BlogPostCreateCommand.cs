﻿using EcoPart.Web.UI.AppCode.Extensions;
using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.BlogPostModule
{
    public class BlogPostCreateCommand : IRequest<BlogPost>
    {
        public string Title { get; set; }
        public string Paragraph { get; set; }
        public string ImagePath { get; set; }
        public int? CategoryId { get; set; }
        public IFormFile File { get; set; }
        //public int[] TagIds { get; set; }
        public class BlogCreateCommandHandler : IRequestHandler<BlogPostCreateCommand, BlogPost>
        {

            readonly EcoPartsDbContext db;
            readonly IWebHostEnvironment env;
            readonly IActionContextAccessor ctx;
            public BlogCreateCommandHandler(EcoPartsDbContext db, IWebHostEnvironment env, IActionContextAccessor ctx)
            {
                this.db = db;
                this.env = env;
                this.ctx = ctx;
            }
            public async Task<BlogPost> Handle(BlogPostCreateCommand request, CancellationToken cancellationToken)
            {
                if (request?.File == null)
                {
                    ctx.AddModelError("ImagePath", "Image Cannot be empty");
                }
                //if (blog.CategoryId == null)
                //{
                //    ModelState.AddModelError("CategoryId", "Category Name Cannot be empty");
                //}

                if (ctx.ModelIsValid())
                {
                    string fileExtension = Path.GetExtension(request.File.FileName);
                    string name = $"blog-{Guid.NewGuid()}{fileExtension}";
                    string physicalPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", name);
                    using (var fs = new FileStream(physicalPath, FileMode.Create, FileAccess.Write))
                    {
                        await request.File.CopyToAsync(fs, cancellationToken);
                    }
                    var blog = new BlogPost
                    {
                        Title = request.Title,
                        Paragraph = request.Paragraph,
                        CategoryId = request.CategoryId,
                        ImagePath = name
                    };
                    db.Add(blog);
                    await db.SaveChangesAsync(cancellationToken);
                    //int affected = await db.SaveChangesAsync(cancellationToken);

                    //if (affected > 0 && request.TagIds != null && request.TagIds.Length > 0)
                    //{
                    //    foreach (var item in request.TagIds)
                    //    {
                    //        await db.BlogPostTagCloud.AddAsync(new BlogPostTag
                    //        {
                    //            BlogId = blog.Id,
                    //            PostTagId = item
                    //        }, cancellationToken);
                    //    }
                    //    await db.SaveChangesAsync(cancellationToken);
                    //}

                    return blog;
                }

                return null;
            }
        }
    }
}
