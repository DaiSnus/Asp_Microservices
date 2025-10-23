using ProductService.Dal.Models;
using ProductService.Dal.Repository.Interfaces;
using ProductService.Logic.Dtos;
using ProductService.Logic.Dtos.Requests;
using ProductService.Logic.Mapper;
using ProductService.Logic.Services.Inerfaces;

namespace ProductService.Logic.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository productRepository;

    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return productRepository.GetByIdAsync(id, token);
    }

    public Task<Product?> GetByArgsAsync(GetProductRequest request, CancellationToken token)
    {
        return productRepository.GetByArgsAsync(request.Title, request.Category, request.ShopId, token);
    }

    public async Task<Product?> UpdateAsync(Guid id, CreateUpdateProductRequest request, CancellationToken token)
    {
        var product = await  productRepository.GetByIdAsync(id, token);

        if (product == null) return null;

        var newProduct = request.ToEntity();
        newProduct.Id = id;
        
        newProduct.Media = request.Media?.Select(m =>
        {
            var media = m.ToEntity(product.Id);
            media.Id = Guid.NewGuid();
            return media;
        }).ToList() ?? new List<ProductMedia>();
        
        return await productRepository.UpdateWithMediaAsync(id, newProduct, newProduct.Media, token);
    }

    public async Task TopUpAsync(Guid id, int amount, CancellationToken token)
    {
        var product = await productRepository.GetByIdAsync(id, token);
        
        if (product == null) return;
        
        product.ToppedAt =  DateTime.UtcNow;
        product.Amount += amount;
        
        await productRepository.UpdateAsync(product, token);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var product = await productRepository.GetByIdAsync(id, token);

        if (product == null)
            return false;

        await productRepository.DeleteAsync(product, token);
        
        return true;
    }

    public async Task<Product> CreateAsync(CreateUpdateProductRequest request, CancellationToken token)
    {
        var product = request.ToEntity();
        
        product.Id = Guid.NewGuid();//TODO jwt
        product.CreatedAt = DateTime.UtcNow;

        product.Media = request.Media?.Select(m =>
        {
            var media = m.ToEntity(product.Id);
            media.Id = Guid.NewGuid();
            return media;
        }).ToList() ?? new List<ProductMedia>();

        return await productRepository.CreateWithMediaAsync(product, product.Media, token);
    }
}