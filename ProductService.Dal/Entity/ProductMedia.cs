using System.Text.Json.Serialization;

namespace ProductService.Dal.Models;

public class ProductMedia
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Url { get; set; } = "";
    
    [JsonIgnore] public Product? Product { get; set; }
}