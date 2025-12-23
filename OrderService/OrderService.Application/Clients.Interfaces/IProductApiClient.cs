using DefaultNamespace;

namespace OrderService.Application.Clients.Interfaces;

public interface IProductApiClient
{
    Task<ProductDto?> GetProductByIdAsync(Guid id, CancellationToken ct = default);
}