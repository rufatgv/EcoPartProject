using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.ProductModule
{
    public class ProductSingleQuery : IRequest<Product>
    {
        public int Id { get; set; }
        public class ProductSingleQueryHandler : IRequestHandler<ProductSingleQuery, Product>
        {
            readonly EcoPartsDbContext db;
            public ProductSingleQueryHandler(EcoPartsDbContext db)
            {
                this.db = db;
            }
            public async Task<Product> Handle(ProductSingleQuery request, CancellationToken cancellationToken)
            {
                var product = await db.Products
                    .Include(b => b.Category)
                    .ThenInclude(b=>b.Brand)
                    //.Include(b=> b.TagCloud)
                    .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedById == null, cancellationToken);

                return product;
            }
        }
    }
}
