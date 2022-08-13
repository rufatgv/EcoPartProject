using EcoPart.Web.UI.AppCode.Extensions;
using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.ProductModule

{
    public class ProductEditCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public string ForSearch { get; set; }
        public string PartCodeName { get; set; }
        public IFormFile File { get; set; }


        public class ProductEditCommandHandler : IRequestHandler<ProductEditCommand, Product>
        {
            readonly EcoPartsDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IWebHostEnvironment env; 

            public ProductEditCommandHandler(EcoPartsDbContext db,
                IActionContextAccessor ctx,
                IWebHostEnvironment env)
            {
                this.db = db;
                this.ctx = ctx;
                this.env = env;
            }

            public async Task<Product> Handle(ProductEditCommand request, CancellationToken cancellationToken)
            {
                if (ctx.ModelIsValid())
                {
                    if (request.File == null && string.IsNullOrEmpty(request.ImagePath))
                    {
                        ctx.AddModelError("ImagePath", "Image Cannot be empty");
                    }
                    var product = await db.Products
                    .FirstOrDefaultAsync(p => p.Id == request.Id && p.DeletedById == null, cancellationToken);
                    if (product == null)
                    {
                        return null;
                    }
                    
                    string oldFileName = product.ImagePath;
                    if (request.File != null)
                    {
                        string fileExtension = Path.GetExtension(request.File.FileName);
                        string name = $"product-{Guid.NewGuid()}{fileExtension}";
                        string physicalPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", name);
                        using (var fs = new FileStream(physicalPath, FileMode.Create, FileAccess.Write))
                        {
                            request.File.CopyTo(fs);
                        }
                        product.ImagePath = name;
                        string physicalPathOld = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", oldFileName);
                        if (System.IO.File.Exists(physicalPathOld))
                        {
                            System.IO.File.Delete(physicalPathOld);
                        }
                    }
                    product.Name = request.Name;
                    product.CategoryId = request.CategoryId;
                    product.Price = request.Price;
                    await db.SaveChangesAsync(cancellationToken);
                    return product;
                }

                return null;
            }
        }
    }


}
