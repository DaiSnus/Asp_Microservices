using ProductService.Dal.Models;
using ProductService.Logic.Dtos;
using ProductService.Logic.Dtos.Requests;

namespace ProductService.Logic.Services.Inerfaces;

public interface IProductService
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken token);
    Task<Product?> GetByArgsAsync(GetProductRequest request, CancellationToken token);
    Task<Product?> UpdateAsync(Guid id, CreateUpdateProductRequest request, CancellationToken token);
    Task TopUpAsync(Guid id, int amount, CancellationToken token);
    Task<bool> DeleteAsync(Guid id, CancellationToken token);
    Task<Product> CreateAsync(CreateUpdateProductRequest request, CancellationToken token);
}