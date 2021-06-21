using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dotnet.Shopping.Portal.Models;
using Dotnet.Shopping.Portal.Models.Catalog;
using Dotnet.Shopping.Portal.Models.Messages;
using Dotnet.Shopping.Portal.Models.Sale;
using Dotnet.Shopping.Portal.Models.Statistics;
using Dotnet.Shopping.Portal.Models.User;

namespace Dotnet.Shopping.Portal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; }
        public DbSet<ProductImageMapping> ProductImageMappings { get; set; }
        public DbSet<ProductManufacturerMapping> ProductManufacturerMappings { get; set; }
        public DbSet<ProductSpecificationMapping> ProductSpecificationMappings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Specification> Specifications { get; set; }

        public DbSet<OrderCount> OrderCounts { get; set; }
        public DbSet<VisitorCount> VisitorCounts { get; set; }

        public DbSet<ContactUsMessage> ContactUsMessage { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BillingAddress>().ToTable("BillingAddress");
            builder.Entity<Category>().ToTable("Category");
            builder.Entity<Image>().ToTable("Image");
            builder.Entity<Manufacturer>().ToTable("Manufacturer");
            builder.Entity<Order>().ToTable("Order");
            builder.Entity<OrderItem>().ToTable("OrderItem");
            builder.Entity<Product>().ToTable("Product");
            builder.Entity<ProductCategoryMapping>().ToTable("ProductCategoryMapping");
            builder.Entity<ProductImageMapping>().ToTable("ProductImageMapping");
            builder.Entity<ProductManufacturerMapping>().ToTable("ProductManufacturerMapping");
            builder.Entity<ProductSpecificationMapping>().ToTable("ProductSpecificationMapping");
            builder.Entity<Review>().ToTable("Review");
            builder.Entity<Specification>().ToTable("Specification");

            builder.Entity<OrderCount>().ToTable("OrderCount");
            builder.Entity<VisitorCount>().ToTable("VisitorCount");

            builder.Entity<ContactUsMessage>().ToTable("ContactUsMessage");
            builder.HasDefaultSchema("Identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
    }
}
