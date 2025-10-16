namespace OrderService.Domain.Models;

public class ProductSnapshot
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public DateTime UpdatedAt { get; set; }
}