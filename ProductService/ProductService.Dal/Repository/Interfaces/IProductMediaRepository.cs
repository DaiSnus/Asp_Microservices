using ProductService.Dal.Models;

namespace ProductService.Dal.Repository.Interfaces;

public interface IProductMediaRepository
{
    Task<IReadOnlyList<ProductMedia>> GetByProductIdAsync(Guid productId, CancellationToken token);
    Task<ProductMedia?> GetByIdAsync(Guid id, CancellationToken token);
    Task AddRangeAsync(IEnumerable<ProductMedia> productMedia, CancellationToken token);
    Task DeleteAsync(ProductMedia media, CancellationToken token);
    Task SaveChangesAsync(CancellationToken token);
}