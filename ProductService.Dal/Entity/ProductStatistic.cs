using System.Text.Json.Serialization;
using ProductService.Dal.Models.Enums;

namespace ProductService.Dal.Models;

public class ProductStatistic
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int TotalSales { get; set; }
    public Period Period { get; set; }
    public DateOnly PeriodStart { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalMargin { get; set; }
    
    [JsonIgnore] public Product? Product { get; set; }
}