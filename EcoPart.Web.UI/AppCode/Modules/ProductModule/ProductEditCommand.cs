﻿//using EcoPart.Web.UI.Models.DataContexts;
//using FluentValidation;
//using FluentValidation.Results;
//using MediatR;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Infrastructure;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace EcoPart.Web.UI.AppCode.Modules.ProductModule

//{
//    public class ProductEditCommand : IRequest<ProductEditCommandResponse>
//    {
//        public int Id { get; set; }
//        public string Name { get; set; }
//        public int CategoryId { get; set; }
//        public double Price { get; set; }
//        public string Values { get; set; }
//        public string Ids { get; set; }
//        public IFormFile File { get; set; }
//        public string ImagePath { get; set; }
//        public string ForSearch { get; set; }


//        public class ProductEditCommandHandler : IRequestHandler<ProductEditCommand, ProductEditCommandResponse>
//        {
//            readonly EcoPartsDbContext db;
//            readonly IActionContextAccessor ctx;
//            readonly IHostingEnvironment env;
//            readonly IValidator<ProductEditCommand> validator;

//            public ProductEditCommandHandler(EcoPartsDbContext db,
//                IActionContextAccessor ctx,
//                IHostingEnvironment env,
//                IValidator<ProductEditCommand> validator)
//            {
//                this.db = db;
//                this.ctx = ctx;
//                this.env = env;
//                this.validator = validator;
//            }

//            public async Task<ProductEditCommandResponse> Handle(ProductEditCommand request, CancellationToken cancellationToken)
//            {
//                var result = validator.Validate(request);

//                var response = new ProductEditCommandResponse
//                {
//                    Product = null,
//                    ValidationResult = null
//                };

//                if (!result.IsValid)
//                {
//                    response.ValidationResult = result;

//                    return response;
//                }

//                var product = await db.Products
//                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
//                if (product == null)
//                {
//                    response.ValidationResult = result;
//                    response.ValidationResult.Errors.Add(new ValidationFailure("Name", "Product tapilmadi"));
//                    return response;
//                }


//                product.Name = request.Name;
//                product.CategoryId = request.CategoryId;


//                if (request.ImagePath != null)
//                {
//                    if (product.ImagePath == null)
//                    {
//                        product.ImagePath = new List<>();
//                    }

//                    foreach (var productFile in request.Images)
//                    {
//                        if (productFile.File == null && string.IsNullOrWhiteSpace(productFile.TempPath))
//                        {
//                            var dbProductImage = product.Images.FirstOrDefault(p => p.Id == productFile.Id);

//                            if (dbProductImage != null)
//                            {
//                                dbProductImage.DeletedById = 1;
//                                dbProductImage.DeletedDate = DateTime.UtcNow.AddHours(4);
//                                dbProductImage.IsMain = false;
//                            }
//                        }
//                        else if (productFile.File != null)
//                        {
//                            string name = await env.SaveFile(productFile.File, cancellationToken, "product");

//                            product.Images.Add(new ProductImage
//                            {
//                                ImagePath = name,
//                                IsMain = productFile.IsMain
//                            });
//                        }
//                        else if (!string.IsNullOrWhiteSpace(productFile.TempPath))
//                        {
//                            var dbProductImage = product.Images.FirstOrDefault(p => p.Id == productFile.Id);

//                            if (dbProductImage != null)
//                            {
//                                dbProductImage.IsMain = productFile.IsMain;
//                            }
//                        }
//                    }
//                }
//                else
//                {
//                    ctx.AddModelError("Images", "Sekil qeyd edilmeyib");
//                    goto l1;
//                }


//                if (request.Specification != null && request.Specification.Length > 0)
//                {
//                    if (product.Specifications == null)
//                    {
//                        product.Specifications = new List<ProductSpecification>();

//                    }

//                    foreach (var spec in product.Specifications)
//                    {
//                        var specFromForm = request.Specification.FirstOrDefault(s => s.Id == spec.SpecificationId);

//                        if (specFromForm == null || string.IsNullOrWhiteSpace(specFromForm.Value))
//                        {
//                            db.ProductSpecifications.Remove(spec);
//                        }
//                        else
//                        {
//                            spec.Value = specFromForm.Value;
//                        }

//                    }
//                    var ids = request.Specification.Select(s => s.Id)
//                        .Except(product.Specifications.Select(s => s.SpecificationId));

//                    foreach (var id in ids)
//                    {
//                        var spec = request.Specification.FirstOrDefault(s => s.Id == id);

//                        product.Specifications.Add(new ProductSpecification
//                        {
//                            SpecificationId = spec.Id,
//                            Value = spec.Value
//                        });
//                    }

//                }

//                //if (request.Pricing != null && request.Pricing.Length > 0)
//                //{
//                //    product.Pricings = new List<Models.Entities.ProductPricing>();

//                //    foreach (var pricing in request.Pricing)
//                //    {
//                //        product.Pricings.Add(new Models.Entities.ProductPricing
//                //        {
//                //            ColorId = pricing.ColorId,
//                //            SizeId = pricing.SizeId,
//                //            Price = pricing.Price
//                //        });
//                //    }
//                //}

//                try
//                {
//                    await db.SaveChangesAsync(cancellationToken);
//                    response.Product = product;
//                    response.ValidationResult = result;
//                    return response;
//                }
//                catch (Exception ex)
//                {
//                    response.Product = product;
//                    response.ValidationResult = result;

//                    response.ValidationResult.Errors.Add(new ValidationFailure("Name", "Xeta bash verdi,Biraz sonra yeniden yoxlayin"));

//                    return response;
//                }

//            l1:
//                return null;
//            }
//        }
//    }


//    public class ProductEditCommandResponse
//    {
//        public Product Product { get; set; }
//        public ValidationResult ValidationResult { get; set; }
//    }
//}
