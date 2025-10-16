using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Models;
using OrderService.Domain.Repository.Interfaces;
using OrderService.Infrastructure.DataLayer;

namespace OrderService.Infrastructure.Repository.Implementations;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext dbContext;

    public OrderRepository(OrderDbContext orderDbContext)
    {
        dbContext = orderDbContext;
    }
    
    public async Task<Order?> GetByIdAsync(Guid orderId)
    {
        return await dbContext.Orders.Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<List<Order>> GetByShopIdAsync(Guid shopId)
    {
        return await dbContext.Orders.Include(o => o.Items)
            .Where(o => o.ShopId == shopId)
            .ToListAsync();
    }

    public async Task<List<Order>> GetByBuyerIdAsync(Guid buyerId)
    {
        return await dbContext.Orders.Include(o => o.Items)
            .Where(o => o.BuyerId == buyerId)
            .ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync();
    }
}