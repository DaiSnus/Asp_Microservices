namespace OrderService.Application.OrderService.Application.Dtos;

public class CreateOrderDto
{
    public Guid BuyerId { get; set; }
    public Guid ShopId { get; set; }
    public List<CreateOrderItemDto> Items { get; set; }
}