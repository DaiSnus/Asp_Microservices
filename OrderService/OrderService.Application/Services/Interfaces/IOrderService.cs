using OrderService.Application.OrderService.Application.Dtos;
using OrderService.Domain.Enums;
using OrderService.Domain.Models;

namespace OrderService.Application.Services.Interfaces;

public interface IOrderService
{
    Task<Guid> CreateOrderAsync(CreateOrderDto dto);
    Task<Order?> GetByIdAsync (Guid orderId);
    Task UpdateStatus (Guid orderId, OrderStatus status);
    Task<List<Order>> GetShopOrders(Guid shopId);
    Task<List<Order>> GetBuyerOrders(Guid buyerId);
    Task UpdateOrderItemsAsync(Guid orderId, UpdateItemsDto items);
}