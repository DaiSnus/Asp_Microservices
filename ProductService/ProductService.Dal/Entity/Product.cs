using ProductService.Dal.Models.Enums;

namespace ProductService.Dal.Models;

public class Product
{
    public Guid Id { get; set; }
    public Guid ShopId { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public int Amount { get; set; }
    public Category Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ToppedAt { get; set; }
    
    public virtual ProductStatistic Statistic { get; set; }
    public virtual IEnumerable<ProductMedia> Media { get; set; }
}