using ProductService.Dal.Repository.Interfaces;
using ProductService.Logic.Dtos;
using ProductService.Logic.Dtos.Requests;
using ProductService.Logic.Mapper;
using ProductService.Logic.Services.Inerfaces;

namespace ProductService.Logic.Services.Implementations;

public class ProductStatisticService :IProductStatisticService
{
    private readonly IProductStatisticRepository productStatisticRepository;
    
    public ProductStatisticService(IProductStatisticRepository productStatisticRepository)
    {
        this.productStatisticRepository = productStatisticRepository;
    }
    
    public async Task<ProductStatisticResponce?> GetAsync(GetStatisticRequest request, CancellationToken token)
    {
        var statistic = await productStatisticRepository.GetByArgsAsync(request.ProductId, request.Period, request.PeriodStart, token);
        
        return statistic == null ? null : statistic?.ToResponce();
    }
}