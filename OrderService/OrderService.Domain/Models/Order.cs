using OrderService.Domain.Enums;

namespace OrderService.Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public Guid ShopId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public OrderStatus Status { get; set; }
    
    public virtual IEnumerable<OrderItem> Items { get; set; }
}