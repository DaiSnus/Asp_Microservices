namespace DefaultNamespace;

public class ProductDto
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public string Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ToppedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}