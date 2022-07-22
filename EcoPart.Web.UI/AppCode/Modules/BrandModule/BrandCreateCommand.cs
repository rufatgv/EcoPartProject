using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.BrandModule
{
    public class BrandCreateCommand : IRequest<Brand>
    {
        public string Name { get; set; }

        public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommand, Brand>
        {
            readonly EcoPartsDbContext db;
            public BrandCreateCommandHandler(EcoPartsDbContext db)
            {
                this.db = db;
            }
            public async Task<Brand> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
            {
               var brand = new Brand();
                brand.Name = request.Name;

               await db.Brands.AddAsync(brand);

                await db.SaveChangesAsync(cancellationToken);

                return brand;
                
            }
        }
    }
}
