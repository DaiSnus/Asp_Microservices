using DefaultNamespace;
using Microsoft.AspNetCore.Identity;
using OrderService.Application.Clients.Interfaces;
using OrderService.Application.OrderService.Application.Dtos;
using OrderService.Application.Services.Interfaces;
using OrderService.Domain.Enums;
using OrderService.Domain.Models;
using OrderService.Domain.Repository.Interfaces;

namespace OrderService.Application.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IProductApiClient _productApiClient;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IProductApiClient productApiClient)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _productApiClient = productApiClient;
    }
    
    public async Task<Guid> CreateOrderAsync(CreateOrderDto dto)
    {
        if (dto.Items.Count == 0)
            throw new ArgumentException("No items found in order.", nameof(dto.Items));
        
        var products = new List<OrderItem>();
        foreach (var item in dto.Items)
        {
            var product = await _productApiClient.GetProductByIdAsync(item.ProductId);
            if (product == null)
                throw new InvalidOperationException($"Product with id {item.ProductId} not available.");
            
            products.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = product.Price,
            });
        }

        var order = new Order
        {
            Items = products,
            BuyerId = dto.BuyerId,
            ShopId = dto.ShopId,
        };

        await _orderRepository.AddAsync(order);
        
        return order.Id;
    }

    public async Task<Order?> GetByIdAsync(Guid orderId)
    {
        return await _orderRepository.GetByIdAsync(orderId);
    }

    public async Task UpdateStatus(Guid orderId, OrderStatus status)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
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
        
        await _orderRepository.UpdateAsync(order);
    }

    public async Task<List<Order>> GetShopOrders(Guid shopId)
    {
        return await _orderRepository.GetByShopIdAsync(shopId);
    }

    public async Task<List<Order>> GetBuyerOrders(Guid buyerId)
    {
        return await _orderRepository.GetByBuyerIdAsync(buyerId);
    }

    public async Task UpdateOrderItemsAsync(Guid orderId, UpdateItemsDto items)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null) 
            throw new InvalidOperationException("Order not found");
        
        if (order.Status == OrderStatus.Canceled || order.Status == OrderStatus.Paid)
            throw new InvalidOperationException("Order status cannot allow to change items");
        
        var newItems = new List<OrderItem>();
        foreach (var item in items.Items)
        {
            var productSnapshot = await _productRepository.GetByIdAsync(item.ProductId);
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
        await _orderRepository.UpdateAsync(order);
    }
    
    [Obsolete]
    public async Task<Order> CreateOrderObsoleteAsync(CreateOrderDto dto)
    {
        if (dto.Items == null || dto.Items.Count == 0)
            throw new InvalidOperationException("Order Items cannot be null or empty.");
        
        var orderItems =  new List<OrderItem>();
        foreach (var item in dto.Items)
        {
            var productSnapshot = await _productRepository.GetByIdAsync(item.ProductId);
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
        
        await _orderRepository.AddAsync(newOrder);
        return newOrder;
    }
}