
using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Models;
using OrderService.Domain.Repository.Interfaces;
using OrderService.Infrastructure.DataLayer;

namespace OrderService.Infrastructure.Repository.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly OrderDbContext dbContext;

    public ProductRepository(OrderDbContext orderDbContext)
    {
        dbContext = orderDbContext;
    }
    
    public async Task<ProductSnapshot?> GetByIdAsync(Guid productId)
    {
        return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
    }

    public async Task UpsertAsync(ProductSnapshot product)
    {
        var existsProduct = await dbContext.Products.FindAsync(product.Id);
        if (existsProduct == null)
        {
            await dbContext.Products.AddAsync(product);
        }
        else
        {
            existsProduct.Title = product.Title;
            existsProduct.Description = product.Description;
            existsProduct.Price = product.Price;
            existsProduct.UpdatedAt = DateTime.UtcNow;
            
            dbContext.Products.Update(existsProduct);
        }
        
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid productId)
    {
        var existsProduct = await dbContext.Products.FindAsync(productId);
        if (existsProduct != null)
        {
            dbContext.Products.Remove(existsProduct);
            await dbContext.SaveChangesAsync();
        }
    }
}