using ProductService.Dal.Models.Enums;

namespace ProductService.Logic.Dtos.Requests;

public class CreateUpdateProductRequest
{
    public string Title { get; init; } = "";
    public Guid ShopId { get; init; }
    public string Description { get; init; } = "";
    public decimal Price { get; init; }
    public int Amount { get; init; }
    public Category Category { get; init; }

    public virtual IEnumerable<CreateProductMediaRequest> Media { get; set; } = [];
}