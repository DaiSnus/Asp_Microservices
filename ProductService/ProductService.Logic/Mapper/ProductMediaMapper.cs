using ProductService.Dal.Models;
using ProductService.Logic.Dtos;
using ProductService.Logic.Dtos.Requests;

namespace ProductService.Logic.Mapper;

public static class ProductMediaMapper
{
    public static ProductMediaResponce ToDto(this ProductMedia media)
    {
        return new ProductMediaResponce
        {
            Id = media.Id,
            Url = media.Url,
        };
    }

    public static ProductMedia ToEntity(this CreateProductMediaRequest responce, Guid productId)
    {
        return new ProductMedia
        {
            Url = responce.Url,
            ProductId = productId
        };
    }
}