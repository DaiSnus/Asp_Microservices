using ProductService.Dal.Models;
using ProductService.Dal.Models.Enums;

namespace ProductService.Logic.Dtos;

public class ProductResponce
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
    
    public IEnumerable<ProductMediaResponce> Media { get; set; } =  new List<ProductMediaResponce>();
    public ProductStatisticResponce Statistic { get; set; } = null!;
}