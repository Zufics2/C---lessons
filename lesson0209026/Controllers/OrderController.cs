using lesson0209026.DTOs;
using lesson0209026.Entities;
using lesson0209026.Services;
using Microsoft.AspNetCore.Mvc;

namespace lesson0209026.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        var orders = await orderService.GetOrders();
        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await orderService.GetOrder(id);

        if (order == null)
        {
            return NotFound($"Заказ с Id {id} не найден.");
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> AddOrder([FromBody] CreateOrderDTO dto)
    {
        var createdOrder = await orderService.AddOrderAsync(dto);
        return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
    }

    [HttpPut]
    public async Task<ActionResult<Order>> UpdateOrder([FromBody] UpdateOrderDTO dto)
    {
        var updatedOrder = await orderService.UpdateOrderAsync(dto);

        if (updatedOrder == null)
        {
            return NotFound($"Заказ с Id {dto.Id} не найден.");
        }

        return Ok(updatedOrder);
    }
}