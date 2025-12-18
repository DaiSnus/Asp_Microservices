namespace OrderService.Application.OrderService.Application.Dtos;

public class UpdateItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}