using Microsoft.EntityFrameworkCore;
using ProductService.Dal.Data;
using ProductService.Dal.Models;
using ProductService.Dal.Models.Enums;
using ProductService.Dal.Repository.Interfaces;

namespace ProductService.Dal.Repository.Implementations;

public class ProductRepository :IProductRepository
{
    private readonly ProductDbContext context;

    public ProductRepository(ProductDbContext context)
    {
        this.context = context;
    }
    
    public async Task<Product> CreateWithMediaAsync(Product product, IEnumerable<ProductMedia>? media,
        CancellationToken token)
    {
        await context.Products.AddAsync(product, token);

        await context.ProductStatistics.AddAsync(new ProductStatistic { ProductId = product.Id, }, token);
            
        if (media != null)
        {
            var productMedia = media.ToList();
            productMedia.ToList().ForEach(m =>
            {
                m.Id = m.Id == Guid.Empty ? Guid.NewGuid() : m.Id;
                m.ProductId = product.Id;
            });
            
            await context.ProductsMedia.AddRangeAsync(productMedia, token);
        }
        
        await context.SaveChangesAsync(token);
        return product;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return await context.Products.Include(p => p.Media)
            .Include(p => p.Statistic)
            .FirstOrDefaultAsync(p => p.Id == id, token);;
    }

    public async Task<Product?> GetByArgsAsync(string? title, Category? category, Guid? shopId, CancellationToken token)
    {
        var query = context.Products.AsQueryable();
        
        if (!string.IsNullOrEmpty(title))
            query = query.Where(p => p.Title.Contains(title));

        if (category != null)
            query = query.Where(p => p.Category == category);
        
        if (shopId != null)
            query = query.Where(p => p.ShopId == shopId);
        
        return await query.FirstOrDefaultAsync(token);
    }

    public async Task UpdateAsync(Product product, CancellationToken token)
    {
        context.Products.Update(product);
        
        await context.SaveChangesAsync(token);
    }

    public async Task<Product?> UpdateWithMediaAsync(Guid productId, Product product, IEnumerable<ProductMedia>? media, CancellationToken token)
    {
        var existingProduct = context.Products.Include(p => p.Media)
            .FirstOrDefault(p => p.Id == productId);
        
        if (existingProduct == null) 
            return null;
        
        existingProduct.Title = product.Title;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;
        existingProduct.Category = product.Category;
        existingProduct.Amount = product.Amount;
        existingProduct.ShopId = product.ShopId;
        existingProduct.ToppedAt = product.ToppedAt;

        if (media != null)
        {
            var lastMedia = context.ProductsMedia.Where(p => p.ProductId == productId);
            context.ProductsMedia.RemoveRange(lastMedia);
            
            var productMedia = media as ProductMedia[] ?? media.ToArray();
            productMedia.ToList().ForEach(m =>
            {
                m.Id = m.Id == Guid.Empty ? Guid.NewGuid() : m.Id;
                m.ProductId = productId;
            });
            
            await context.ProductsMedia.AddRangeAsync(productMedia);
        }
        
        await context.SaveChangesAsync(token);
        return existingProduct;
    } 

    public async Task DeleteAsync(Product product, CancellationToken token)
    {
        context.Products.Remove(product);
        
        await context.SaveChangesAsync(token);
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        await context.SaveChangesAsync(token);
    }
}