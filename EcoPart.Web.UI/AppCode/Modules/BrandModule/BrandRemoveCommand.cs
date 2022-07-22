using EcoPart.Web.UI.AppCode.Infrastructure;
using EcoPart.Web.UI.Models.DataContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.BrandModule
{
    public class BrandRemoveCommand : IJsonRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class BrandRemoveCommandHandler : IRequestHandler<BrandRemoveCommand, CommandJsonResponse>
        {

            readonly EcoPartsDbContext db;
            public BrandRemoveCommandHandler(EcoPartsDbContext db)
            {
                this.db = db;
            }


            public async Task<CommandJsonResponse> Handle(BrandRemoveCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.Brands
                    .FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedById == null, cancellationToken);
                if (entity == null)
                {
                    return new CommandJsonResponse(true, "Qeyd Movcud Deil");
                }

                entity.DeletedById = 1;
                entity.DeletedDate = DateTime.UtcNow.AddHours(4);

                await db.SaveChangesAsync(cancellationToken);

                return new CommandJsonResponse(false, "Qeyd Silindi");
            }

        }



    }
}
