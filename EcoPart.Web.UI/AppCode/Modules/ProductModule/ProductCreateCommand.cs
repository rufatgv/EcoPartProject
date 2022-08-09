using EcoPart.Web.UI.AppCode.Extensions;
using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.ProductModule
{
    public class ProductCreateCommand : IRequest<ProductCreateCommandResponse>
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public string Values { get; set; }
        public string Ids { get; set; }
        public IFormFile File { get; set; }
        public string ImagePath { get; set; }
        public string ForSearch { get; set; }
        public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, ProductCreateCommandResponse>
        {

            readonly EcoPartsDbContext db;
            readonly IActionContextAccessor ctx;
            readonly IWebHostEnvironment env;
            readonly IValidator<ProductCreateCommand> validator;

            public ProductCreateCommandHandler(EcoPartsDbContext db,
                IActionContextAccessor ctx,
                IWebHostEnvironment env,
                IValidator<ProductCreateCommand> validator)
            {
                this.db = db;
                this.ctx = ctx;
                this.env = env;
                this.validator = validator;
            }

            public async Task<ProductCreateCommandResponse> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
            {
                if (request?.File == null)
                {
                    ctx.AddModelError("ImagePath", "Image Cannot be empty");
                }

                var result = validator.Validate(request);



                if (!result.IsValid)
                {
                    var response = new ProductCreateCommandResponse
                    {
                        Product = null,
                        ValidationResult = result
                    };

                    return response;
                }

                var product = new Product();

                product.Name = request.Name;
                product.CategoryId = request.CategoryId;
                product.Price = request.Price;


                if (request.Values != null && request.Values.Length > 0)
                {
                    product.PartCodeName = request.Values;
                    product.ForSearch = request.Name + request.Values;
                }

                if (request.Ids != null && request.Ids.Length > 0)
                {
                    product.PartCodeIds = request.Ids;
                }

                string fileExtension = Path.GetExtension(request.File.FileName);
                string name = $"product-{Guid.NewGuid()}{fileExtension}";
                string physicalPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", name);
                using (var fs = new FileStream(physicalPath, FileMode.Create, FileAccess.Write))
                {
                    await request.File.CopyToAsync(fs, cancellationToken);
                }
                product.ImagePath = name;

                

                await db.Products.AddAsync(product, cancellationToken);
                try
                {
                    await db.SaveChangesAsync(cancellationToken);
                    var response = new ProductCreateCommandResponse
                    {
                        Product = product,
                        ValidationResult = result
                    };
                    return response;
                }
                catch (Exception ex)
                {
                    var response = new ProductCreateCommandResponse
                    {
                        Product = product,
                        ValidationResult = result
                    };

                    response.ValidationResult.Errors.Add(new ValidationFailure("Name", "Xeta bash verdi,Biraz sonra yeniden yoxlayin"));

                    return response;
                }

            l1:
                return null;
            }
        }
    }




    public class ProductCreateCommandResponse
    {
        public Product Product { get; set; }
        public ValidationResult ValidationResult { get; set; }
    }
}
