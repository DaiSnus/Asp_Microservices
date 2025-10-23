using ProductService.Dal.Models;
using ProductService.Logic.Dtos;

namespace ProductService.Logic.Mapper;

public static class ProductStatisticMapper
{
    public static ProductStatisticResponce ToResponce(this ProductStatistic productStatistic)
    {
        return new ProductStatisticResponce
        {
            Id = productStatistic.Id,
            TotalRevenue = productStatistic.TotalRevenue,
            TotalMargin = productStatistic.TotalMargin,
            TotalSales = productStatistic.TotalSales,
            Period = productStatistic.Period,
            PeriodStart = productStatistic.PeriodStart,
        };
    }

    public static ProductStatistic ToEntity(this ProductStatisticResponce productStatisticResponce, Guid productId)
    {
        return new ProductStatistic
        {
            Id = productStatisticResponce.Id,
            TotalRevenue = productStatisticResponce.TotalRevenue,
            TotalMargin = productStatisticResponce.TotalMargin,
            TotalSales = productStatisticResponce.TotalSales,
            Period = productStatisticResponce.Period,
            PeriodStart = productStatisticResponce.PeriodStart,
            ProductId = productId,
        };
    }
}