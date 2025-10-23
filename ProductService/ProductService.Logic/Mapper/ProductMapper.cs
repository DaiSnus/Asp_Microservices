using ProductService.Dal.Models;
using ProductService.Logic.Dtos;
using ProductService.Logic.Dtos.Requests;

namespace ProductService.Logic.Mapper;

public static class ProductMapper
{
    public static ProductResponce ToDto(this Product product)
    {
        return new ProductResponce
        {
            Id = product.Id,
            ShopId = product.ShopId,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            Amount = product.Amount,
            Category = product.Category,
            CreatedAt = product.CreatedAt,
            ToppedAt = product.ToppedAt,
        };
    }

    public static Product ToEntity(this CreateUpdateProductRequest productRequest)
    {
        return new Product
        {
            ShopId = productRequest.ShopId,
            Title = productRequest.Title,
            Description = productRequest.Description,
            Price = productRequest.Price,
            Amount = productRequest.Amount,
            Category = productRequest.Category,
        };
    }
}