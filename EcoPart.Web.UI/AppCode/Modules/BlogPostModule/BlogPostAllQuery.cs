using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.BlogPostModule
{
    public class BlogPostAllQuery : IRequest<IEnumerable<BlogPost>>
    {
        public class BlogAllQueryHandler : IRequestHandler<BlogPostAllQuery, IEnumerable<BlogPost>>
        {
            readonly EcoPartsDbContext db;
            public BlogAllQueryHandler(EcoPartsDbContext db)
            {
                this.db = db;
            }
            public async Task<IEnumerable<BlogPost>> Handle(BlogPostAllQuery request, CancellationToken cancellationToken)
            {
                var entity = db.BlogPosts
                 .Include(b => b.Category)
                .Where(b => b.DeletedById == null);
                return entity;
            }
        }
    }
}
