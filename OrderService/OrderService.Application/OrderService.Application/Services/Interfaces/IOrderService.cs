using OrderService.Application.OrderService.Application.Dtos;
using OrderService.Domain.Enums;
using OrderService.Domain.Models;

namespace DefaultNamespace;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(CreateOrderDto dto);
    Task<Order?> GetByIdAsync (Guid orderId);
    Task UpdateStatus (Guid orderId, OrderStatus status);
    Task<List<Order>> GetShopOrders(Guid shopId);
    Task<List<Order>> GetBuyerOrders(Guid buyerId);
    Task UpdateOrderItemsAsync(Guid orderId, UpdateItemsDto items);
}