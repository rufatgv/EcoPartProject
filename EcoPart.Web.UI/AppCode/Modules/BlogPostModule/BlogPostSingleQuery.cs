using EcoPart.Web.UI.Models.DataContexts;
using EcoPart.Web.UI.Models.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.BlogPostModule
{
    public class BlogPostSingleQuery : IRequest<BlogPost>
    {
        public int Id { get; set; }
        public class BlogSingleQueryHandler : IRequestHandler<BlogPostSingleQuery, BlogPost>
        {
            readonly EcoPartsDbContext db;
            public BlogSingleQueryHandler(EcoPartsDbContext db)
            {
                this.db = db;
            }
            public async Task<BlogPost> Handle(BlogPostSingleQuery request, CancellationToken cancellationToken)
            {

                var blog = await db.BlogPosts
                    .Include(b => b.Category)
                    //.Include(b=> b.TagCloud)
                    .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedById == null, cancellationToken);

                return blog;
            }
        }
    }
}
