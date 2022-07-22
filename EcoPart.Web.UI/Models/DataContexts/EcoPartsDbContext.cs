using EcoPart.Web.UI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoPart.Web.UI.Models.DataContexts
{
    public class EcoPartsDbContext : DbContext
    {
        public EcoPartsDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Brand>Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<BlogPost> BlogPosts { get; set; }

    }
}
