using FoodOrderingSystem.Rider.Application.Contracts;
using FoodOrderingSystem.Rider.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Rider.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RidersController : ControllerBase
{
    private readonly IRiderService _riderService;

    public RidersController(IRiderService riderService)
    {
        _riderService = riderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRider([FromBody] CreateRiderDto dto)
    {
        var rider = await _riderService.CreateRiderAsync(dto);
        return CreatedAtAction(nameof(GetRiderById), new { riderId = rider.Id }, rider);
    }

    [HttpGet("{riderId}")]
    public async Task<IActionResult> GetRiderById(Guid riderId)
    {
        var rider = await _riderService.GetRiderByIdAsync(riderId);
        return rider == null ? NotFound() : Ok(rider);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailableRiders()
    {
        var riders = await _riderService.GetAvailableRidersAsync();
        return Ok(riders);
    }

    [HttpPut("{riderId}/location")]
    public async Task<IActionResult> UpdateLocation(Guid riderId, [FromBody] UpdateRiderLocationDto dto)
    {
        await _riderService.UpdateRiderLocationAsync(riderId, dto);
        return NoContent();
    }

    [HttpPut("{riderId}/status")]
    public async Task<IActionResult> UpdateStatus(Guid riderId, [FromBody] UpdateRiderStatusDto dto)
    {
        await _riderService.UpdateRiderStatusAsync(riderId, dto);
        return NoContent();
    }

    [HttpPost("{riderId}/orders")]
    public async Task<IActionResult> AssignOrder(Guid riderId, [FromBody] AssignOrderDto dto)
    {
        await _riderService.AssignOrderToRiderAsync(riderId, dto);
        return Ok();
    }

    [HttpDelete("{riderId}/orders/{orderId}")]
    public async Task<IActionResult> CompleteOrder(Guid riderId, Guid orderId)
    {
        await _riderService.CompleteOrderAsync(riderId, orderId);
        return NoContent();
    }
} 