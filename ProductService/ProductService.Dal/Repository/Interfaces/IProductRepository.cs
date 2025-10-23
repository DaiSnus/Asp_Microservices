using ProductService.Dal.Models;
using ProductService.Dal.Models.Enums;

namespace ProductService.Dal.Repository.Interfaces;

public interface IProductRepository
{
    Task<Product> CreateWithMediaAsync(Product product, IEnumerable<ProductMedia>? media, CancellationToken token);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken token);
    Task<Product?> GetByArgsAsync(string? title, Category? category, Guid? shopId, CancellationToken token);
    Task UpdateAsync(Product product, CancellationToken token);
    Task<Product?> UpdateWithMediaAsync(Guid productId, Product product, IEnumerable<ProductMedia>? media, CancellationToken token);
    Task DeleteAsync(Product product, CancellationToken token); 
    Task SaveChangesAsync(CancellationToken token);
}