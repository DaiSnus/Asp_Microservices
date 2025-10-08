using ProductService.Logic.Dtos;
using ProductService.Logic.Dtos.Requests;

namespace ProductService.Logic.Services.Inerfaces;

public interface IProductStatisticService
{
    Task<ProductStatisticResponce?> GetAsync(GetStatisticRequest request, CancellationToken token);
}