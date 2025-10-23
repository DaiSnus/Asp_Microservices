using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductService.Dal.Models;
using ProductService.Dal.Models.Enums;

namespace ProductService.Dal.Repository.Interfaces;

public interface IProductStatisticRepository
{
    Task<ProductStatistic?> GetByIdAsync(Guid id, CancellationToken token);
    Task<ProductStatistic?> GetByProductIdAsync(Guid productId, CancellationToken token);
    Task<ProductStatistic?> GetByArgsAsync(Guid productId, Period period, DateOnly periodStart, CancellationToken token);
}