using Microsoft.AspNetCore.Mvc;
using ProductService.Dal.Repository.Interfaces;
using ProductService.Logic.Dtos.Requests;
using ProductService.Logic.Services.Inerfaces;

namespace ProductService.Api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;
    private readonly IProductStatisticService productStatisticService;
    
    public ProductController(IProductService productService , IProductStatisticService productStatisticService)
    {
        this.productService = productService;
        this.productStatisticService = productStatisticService;
    }

    /// <summary>
    /// Получение продуктов по аргументам
    /// </summary>
    /// <param name="request">Аргументы: название, категория, Id магазина, точка выдачи</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Список продуктов</returns>
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromBody] GetProductRequest request, CancellationToken token)
    {
        var products = await productService.GetByArgsAsync(request, token);
        return Ok(products);
    }

    /// <summary>
    /// Получение товара по Id
    /// </summary>
    /// <param name="id">Id Товара</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Продукт</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(Guid id, CancellationToken token)
    {
        var product = await productService.GetByIdAsync(id, token);
        return product == null ? NotFound() : Ok(product);
    }

    /// <summary>
    /// Создание продукта
    /// </summary>
    /// <param name="request">Dto продукта для его создания</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Созданный продукт</returns>
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateProductRequest request,
        CancellationToken token)
    {
        var newProduct = await productService.CreateAsync(request, token);
        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
    }

    /// <summary>
    /// Обновление продукта
    /// </summary>
    /// <param name="id">Id продукта</param>
    /// <param name="request">Dto для обновления</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Обновленный продукт/404 при неверном id</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] CreateUpdateProductRequest request,
        CancellationToken token)
    {
        var updatedProduct = await productService.UpdateAsync(id, request, token);
        return updatedProduct == null ? NotFound() : Ok(updatedProduct);
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken token)
    {
        var isDeleted = await productService.DeleteAsync(id, token);
        return isDeleted ? Ok() : NotFound();
    }

    /// <summary>
    /// Пополнение товара
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="amount">На сколько единиц пополнить</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>No content(204 status)</returns>
    [HttpPost("{id}/topup")]
    public async Task<IActionResult> TopUpProduct(Guid id, [FromQuery] int amount, CancellationToken token)
    { 
        await productService.TopUpAsync(id, amount, token);
        return NoContent();
    }

    /// <summary>
    /// Получение статистики по продажам у данного товара
    /// </summary>
    /// <param name="request">Id продукта, интересующий период и дата начала периода</param>
    /// <param name="token">Токен отмены</param>
    /// <returns>Статистика товара/404 при неверном id</returns>
    [HttpGet("{id}/statistics")]
    public async Task<IActionResult> GetStatistics([FromBody] GetStatisticRequest request, CancellationToken token)
    {
        var statistics = await productStatisticService.GetAsync(request, token);
        return statistics == null ?  NotFound() : Ok(statistics);
    }
}