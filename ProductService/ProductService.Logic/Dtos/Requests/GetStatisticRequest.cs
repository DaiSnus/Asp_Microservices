using ProductService.Dal.Models.Enums;

namespace ProductService.Logic.Dtos.Requests;

public class GetStatisticRequest
{
    public Guid ProductId { get; set; }
    public Period Period { get; set; }
    public DateOnly PeriodStart { get; set; }
}