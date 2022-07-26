using EcoPart.Web.UI.AppCode.Infrastructure;
using EcoPart.Web.UI.Models.DataContexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EcoPart.Web.UI.AppCode.Modules.BlogPostModule
{
    public class BlogPostRemoveCommand : IRequest<CommandJsonResponse>
    {
        public int Id { get; set; }
        public class BlogRemoveCommandHandler : IRequestHandler<BlogPostRemoveCommand, CommandJsonResponse>
        {
            readonly EcoPartsDbContext db;
            public BlogRemoveCommandHandler(EcoPartsDbContext db)
            {
                this.db = db;
            }
            public async Task<CommandJsonResponse> Handle(BlogPostRemoveCommand request, CancellationToken cancellationToken)
            {
                var entity = await db.BlogPosts
                    .FirstOrDefaultAsync(b => b.DeletedById == null && b.Id == request.Id, cancellationToken);

                if (entity==null)
                {
                    return new CommandJsonResponse(true, "Qeyd Movcud Deyil!");
                }

                entity.DeletedById = 1; //helelik
                entity.DeletedDate = DateTime.UtcNow.AddHours(4);
                await db.SaveChangesAsync(cancellationToken);
                return new CommandJsonResponse(false, "Deleted Successfully");
            }
        }
    }
}
