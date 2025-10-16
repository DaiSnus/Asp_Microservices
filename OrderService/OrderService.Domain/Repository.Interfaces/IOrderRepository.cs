using OrderService.Domain.Models;

namespace OrderService.Domain.Repository.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid orderId);
    Task<List<Order>> GetByShopIdAsync(Guid shopId);
    Task<List<Order>> GetByBuyerIdAsync(Guid buyerId);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
}