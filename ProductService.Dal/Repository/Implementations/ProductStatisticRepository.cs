using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProductService.Dal.Data;
using ProductService.Dal.Models;
using ProductService.Dal.Models.Enums;
using ProductService.Dal.Repository.Interfaces;

namespace ProductService.Dal.Repository.Implementations;

public class ProductStatisticRepository : IProductStatisticRepository
{
    private readonly ProductDbContext context;

    public ProductStatisticRepository(ProductDbContext context)
    {
        this.context = context;
    }

    public async Task<ProductStatistic?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return await context.ProductStatistics.FirstOrDefaultAsync(s => s.Id == id, token);
    }

    public async Task<ProductStatistic?> GetByProductIdAsync(Guid productId, CancellationToken token)
    {
        return await context.ProductStatistics.FirstOrDefaultAsync(s => s.ProductId == productId, token);
    }

    public async Task<ProductStatistic?> GetByArgsAsync(Guid productId, Period period, DateOnly periodStart, CancellationToken token)
    {
        return await context.ProductStatistics.AsNoTracking()
            .Where(s => s.Period == period)
            .Where(s => s.PeriodStart == periodStart)
            .FirstOrDefaultAsync(s => s.ProductId == productId, token);
    }
}