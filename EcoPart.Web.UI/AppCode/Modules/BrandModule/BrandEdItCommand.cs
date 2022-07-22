using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.BrandModule
{
    public class BrandEdItCommand : IRequest<Brand>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class BrandEdItCommandHandler : IRequestHandler<BrandEdItCommand, Brand>

        {
            readonly EcoPartsDbContext db;
            public BrandEdItCommandHandler(EcoPartsDbContext db)
            {
                this.db = db;
            }
            public async Task<Brand> Handle(BrandEdItCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Brands.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);
                if(entity==null)
                {
                    return null;
                }
                
                entity.Name = request.Name;
                await db.SaveChangesAsync(cancellationToken);

                
                return entity;

            }
        }
    }
}
