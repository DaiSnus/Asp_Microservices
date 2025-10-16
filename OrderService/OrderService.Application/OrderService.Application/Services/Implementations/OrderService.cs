using OrderService.Application.OrderService.Application.Dtos;
using OrderService.Domain.Enums;
using OrderService.Domain.Models;
using OrderService.Domain.Repository.Interfaces;

namespace DefaultNamespace;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;
    private readonly IProductRepository productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        this.orderRepository = orderRepository;
        this.productRepository = productRepository;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
    {
        if (dto.Items == null || dto.Items.Count == 0)
            throw new InvalidOperationException("Order Items cannot be null or empty.");
        
        var orderItems =  new List<OrderItem>();
        foreach (var item in dto.Items)
        {
            var productSnapshot = await productRepository.GetByIdAsync(item.ProductId);
            if (productSnapshot == null) 
               throw new InvalidOperationException("Product not found.");
                
            orderItems.Add(new OrderItem
            {
                Id = item.ProductId, 
                Quantity = item.Quantity,
                Price = productSnapshot.Price
            });
        }

        var newOrder = new Order
        {
            BuyerId = dto.BuyerId,
            ShopId = dto.ShopId,
            Items = orderItems,
        };
        
        await orderRepository.AddAsync(newOrder);
        return newOrder;
    }

    public async Task<Order?> GetByIdAsync(Guid orderId)
    {
        return await orderRepository.GetByIdAsync(orderId);
    }

    public async Task UpdateStatus(Guid orderId, OrderStatus status)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order == null)
            throw new InvalidOperationException("Order not found");

        switch (status)
        {
            case OrderStatus.Paid:
                order.Status = OrderStatus.Paid;
                break;
            case OrderStatus.Completed:
                order.Status = OrderStatus.Completed;
                break;
            case OrderStatus.Canceled:
                order.Status = OrderStatus.Canceled;
                break;
            default: throw new ArgumentOutOfRangeException("Invalid order status");
        }
        
        await orderRepository.UpdateAsync(order);
    }

    public async Task<List<Order>> GetShopOrders(Guid shopId)
    {
        return await orderRepository.GetByShopIdAsync(shopId);
    }

    public async Task<List<Order>> GetBuyerOrders(Guid buyerId)
    {
        return await orderRepository.GetByBuyerIdAsync(buyerId);
    }

    public async Task UpdateOrderItemsAsync(Guid orderId, UpdateItemsDto items)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order == null) 
            throw new InvalidOperationException("Order not found");
        
        if (order.Status == OrderStatus.Canceled || order.Status == OrderStatus.Paid)
            throw new InvalidOperationException("Order status cannot allow to change items");
        
        var newItems = new List<OrderItem>();
        foreach (var item in items.Items)
        {
            var productSnapshot = await productRepository.GetByIdAsync(item.ProductId);
            if (productSnapshot == null)
                throw new InvalidOperationException("Product not found");
            
            newItems.Add(new OrderItem
            {
                Id = item.ProductId,
                Quantity = item.Quantity,
                Price = productSnapshot.Price
            });
        }
        
        order.Items = newItems;
        await orderRepository.UpdateAsync(order);
    }
}