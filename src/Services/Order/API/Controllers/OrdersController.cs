using FoodOrderingSystem.Order.Application.Contracts;
using FoodOrderingSystem.Order.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        // In a real app, we'd validate the CustomerId from the token
        var order = await _orderService.CreateOrderAsync(dto);
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null) return NotFound();
        // Add auth check: only customer, restaurant owner, or admin can view
        return Ok(order);
    }

    [HttpGet("customer/{customerId:guid}")]
    public async Task<IActionResult> GetOrdersByCustomer(Guid customerId)
    {
        // Add auth check: only the customer or an admin can view
        var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
        return Ok(orders);
    }

    [HttpGet("restaurant/{restaurantId:guid}")]
    public async Task<IActionResult> GetOrdersByRestaurant(Guid restaurantId)
    {
        // Add auth check: only the restaurant owner or an admin can view
        var orders = await _orderService.GetOrdersByRestaurantIdAsync(restaurantId);
        return Ok(orders);
    }

    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusDto dto)
    {
        try
        {
            var order = await _orderService.UpdateOrderStatusAsync(id, dto);
            return Ok(order);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
} 