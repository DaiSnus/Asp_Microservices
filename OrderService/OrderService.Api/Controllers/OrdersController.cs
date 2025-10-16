using Microsoft.AspNetCore.Mvc;
using OrderService.Application.OrderService.Application.Dtos;
using OrderService.Domain.Enums;
using OrderService.Domain.Repository.Interfaces;

namespace DefaultNamespace;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService orderService;
    
    public OrdersController(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    /// <summary>
    /// Получить заказ по Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>404 если заказ не найден, иначе 200 и order</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var order = await orderService.GetByIdAsync(id);
        return order == null ?  NotFound() : Ok(order);
    }

    /// <summary>
    /// Создать заказ
    /// </summary>
    /// <param name="dto">id покупателя, магазина, продукты в заказе с количеством</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOrderDto dto)
    {
        var order = await orderService.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }
    
    /// <summary>
    /// Обновить статус заказа
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="status">статус</param>
    /// <returns>No content</returns>
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody]OrderStatus status)
    {
        await orderService.UpdateStatus(id, status);
        return NoContent();
    }
    
    /// <summary>
    /// Обновить состав заказа
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="dto">Новые продукты</param>
    /// <returns>NoContent</returns>
    [HttpPut("{id}/items")]
    public async Task<IActionResult> UpdateItems(Guid id, [FromBody] UpdateItemsDto dto)
    {
        await orderService.UpdateOrderItemsAsync(id, dto);
        return NoContent();
    }
}