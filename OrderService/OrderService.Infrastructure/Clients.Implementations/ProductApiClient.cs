using Core.HttpLogic.Services;
using Core.HttpLogic.Services.Interfaces;
using DefaultNamespace;
using OrderService.Application.Clients.Interfaces;

namespace OrderService.Infrastructure.Clients.Implementations;

public class ProductApiClient : IProductApiClient
{
    private readonly IHttpRequestService _httpRequestService;
    private readonly HttpConnectionData _httpConnectionData;

    public ProductApiClient(IHttpRequestService httpRequestService)
    {
        _httpConnectionData = new HttpConnectionData
        {
            ClientName = "product-api",
            Timeout = TimeSpan.FromSeconds(30),
        };
        _httpRequestService = httpRequestService;
    }
    
    public async Task<ProductDto?> GetProductByIdAsync(Guid id, CancellationToken ct = default)
    {
        var request = new HttpRequestData
        {
            Method = HttpMethod.Get,
            Uri = new Uri($"api/products/{id}", UriKind.Relative),
            ContentType = ContentType.ApplicationJson
        };

        var response = await _httpRequestService.SendRequestAsync<ProductDto>(request, _httpConnectionData with { CancellationToken = ct});
        
        return response.IsSuccessStatusCode ? response.Body : null;
    }
}