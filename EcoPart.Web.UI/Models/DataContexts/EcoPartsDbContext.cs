using EcoPart.Web.UI.Models.Entities;
using EcoPart.Web.UI.Models.Entities.Membership;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcoPart.Web.UI.Models.DataContexts
{
    public class EcoPartsDbContext : IdentityDbContext<EcoPartsUser,
        EcoPartsRole,
        int,
        EcoPartsUserClaim,
        EcoPartsUserRole,
        EcoPartsUserLogin,
        EcoPartsRoleClaim,
        EcoPartsUserToken>
    {
        public EcoPartsDbContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Brand>Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PartCode> PartCodes { get; set; }
        public DbSet<ProductPricing> ProductPricings { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<BlogPostTag>(e =>
            //{
            //    e.HasKey(k => new { k.BlogPostId, k.PostTagId });
            //});

            //modelBuilder.Entity<ProductSpecification>(e =>
            //{
            //    e.HasKey(k => new { k.ProductId, k.SpecificationId });
            //});

            modelBuilder.Entity<ProductPricing>(e =>
            {
                e.HasKey(k => new { k.TypeId, k.ProductId });
            });


            modelBuilder.Entity<EcoPartsUser>(e =>
            {
                e.ToTable("Users", "Membership");
            });

            modelBuilder.Entity<EcoPartsRole>(e =>
            {
                e.ToTable("Roles", "Membership");
            });

            modelBuilder.Entity<EcoPartsUserClaim>(e =>
            {
                e.ToTable("UserClaims", "Membership");
            });

            modelBuilder.Entity<EcoPartsUserToken>(e =>
            {
                e.HasKey(k => new
                {
                    k.LoginProvider,
                    k.UserId,
                    k.Name
                });
                e.ToTable("UserTokens", "Membership");
            });

            modelBuilder.Entity<EcoPartsRoleClaim>(e =>
            {
                e.ToTable("RoleClaims", "Membership");
            });

            modelBuilder.Entity<EcoPartsUserRole>(e =>
            {
                e.HasKey(k => new
                {
                    k.RoleId,
                    k.UserId,

                });
                e.ToTable("UserRoles", "Membership");
            });

            modelBuilder.Entity<EcoPartsUserLogin>(e =>
            {
                e.HasKey(k => new
                {
                    k.LoginProvider,
                    k.UserId,
                    k.ProviderKey
                });
                e.ToTable("UserLogins", "Membership");
            });

        }

    }
}
