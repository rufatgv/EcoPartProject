using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.BrandModule
{
    public class BrandSingleQuery : IRequest<Brand>
    {
        public int Id { get; set; }

        public class BrandSingleQueryHandler : IRequestHandler<BrandSingleQuery, Brand>
        {
            readonly EcoPartsDbContext db;
            public BrandSingleQueryHandler(EcoPartsDbContext db)
            {
                this.db = db;
            }


            public  async Task<Brand> Handle(BrandSingleQuery request, CancellationToken cancellationToken)
            {
                var model = await db.Brands
                    .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);

                return model;

            }
        }
    }
}
