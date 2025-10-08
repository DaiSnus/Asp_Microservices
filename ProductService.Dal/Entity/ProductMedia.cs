namespace ProductService.Dal.Models;

public class ProductMedia
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Url { get; set; } = "";
    
    public Product? Product { get; set; }
}