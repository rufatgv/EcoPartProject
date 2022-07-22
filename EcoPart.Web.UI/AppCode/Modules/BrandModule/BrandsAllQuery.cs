using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.BrandModule
{
    public class BrandsAllQuery : IRequest<IEnumerable<Brand>>
    {

        public class BrandsAllQueryHandler : IRequestHandler<BrandsAllQuery, IEnumerable<Brand>>
        {
            readonly EcoPartsDbContext db;
            public BrandsAllQueryHandler(EcoPartsDbContext db)
            {
                this.db = db;
            }


            public async Task<IEnumerable<Brand>> Handle(BrandsAllQuery request, CancellationToken cancellationToken)
            {
                var model = db.Brands.Where(b => b.DeletedById == null);

                    return model;
            }
        }

    }
}
