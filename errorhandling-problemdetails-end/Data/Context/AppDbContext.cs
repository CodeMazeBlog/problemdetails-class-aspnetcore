using ErrorHandlingProblemDetails.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ErrorHandlingProblemDetails.Data.Context
{
	public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Product>().Property(p => p.Category).IsRequired().HasMaxLength(20);

            builder.Entity<Product>().HasData
            (
                new Product { Id = 101, Name = "Plate", Category = "Kitchen Utensils" },
                new Product { Id = 102, Name = "Blanket", Category = "Accessories" },
                new Product { Id = 103, Name = "Bicycle", Category = "Vehicle" }
            );

        }
    }
}
