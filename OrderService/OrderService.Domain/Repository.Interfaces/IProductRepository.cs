using OrderService.Domain.Models;

namespace OrderService.Domain.Repository.Interfaces;

public interface IProductRepository
{
    Task<ProductSnapshot?> GetByIdAsync(Guid productId);
    Task UpsertAsync(ProductSnapshot product);
    Task DeleteAsync(Guid productId);
}