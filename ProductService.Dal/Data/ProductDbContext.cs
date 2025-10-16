using Microsoft.EntityFrameworkCore;
using ProductService.Dal.Models;

namespace ProductService.Dal.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options)
        : base(options)
    {
        
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductMedia> ProductsMedia { get; set; }
    public DbSet<ProductStatistic> ProductStatistics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Media)
            .WithOne(m => m.Product)
            .HasForeignKey(m => m.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Statistic)
            .WithOne(p => p.Product)
            .HasForeignKey<ProductStatistic>(p => p.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(modelBuilder);
    }
}