using ProductService.Dal.Models.Enums;

namespace ProductService.Logic.Dtos.Requests;

public class GetProductRequest 
{
    public string? Title { get; set; }
    public Category? Category { get; set; }
    public Guid? ShopId { get; set; }
    public string? PointReceipt { get; set; } // Место получения заказа
}