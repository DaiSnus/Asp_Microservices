using Microsoft.EntityFrameworkCore;
using ProductService.Dal.Data;
using ProductService.Dal.Models;
using ProductService.Dal.Repository.Interfaces;

namespace ProductService.Dal.Repository.Implementations;

public class ProductMediaRepository : IProductMediaRepository
{
    private readonly ProductDbContext context;
    
    public ProductMediaRepository(ProductDbContext context)
    {
        this.context = context;
    }
     
    public async Task<IReadOnlyList<ProductMedia>> GetByProductIdAsync(Guid productId, CancellationToken token)
    {
        return await context.ProductsMedia.Where(media => media.ProductId == productId).ToListAsync(token);
    }

    public async Task<ProductMedia?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return await context.ProductsMedia.FirstOrDefaultAsync(m => m.Id == id, token);
    }

    public async Task AddRangeAsync(IEnumerable<ProductMedia> productMedia, CancellationToken token)
    {
        await context.AddRangeAsync(productMedia, token);
    }

    public Task DeleteAsync(ProductMedia media, CancellationToken token)
    {
        context.ProductsMedia.Remove(media);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken token)
    {
        await context.SaveChangesAsync(token);
    }
}