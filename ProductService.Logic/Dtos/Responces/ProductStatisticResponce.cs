using ProductService.Dal.Models.Enums;

namespace ProductService.Logic.Dtos;

public class ProductStatisticResponce
{
    public Guid Id { get; set; }
    public int TotalSales { get; set; }
    public Period Period { get; set; }
    public DateOnly PeriodStart { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalMargin { get; set; }
}