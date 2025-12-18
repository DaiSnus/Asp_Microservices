namespace OrderService.Application.OrderService.Application.Dtos;

public class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}