using FoodOrderingSystem.Restaurant.Application.Contracts;
using FoodOrderingSystem.Restaurant.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Restaurant.API.Controllers;

[ApiController]
[Route("api/addons")]
public class AddOnsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public AddOnsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAddOns()
    {
        var addOns = await _restaurantService.GetAllAddOnsAsync();
        return Ok(addOns);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAddOnById(Guid id)
    {
        var addOn = await _restaurantService.GetAddOnByIdAsync(id);
        if (addOn == null) return NotFound();
        return Ok(addOn);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAddOn([FromBody] CreateAddOnDto createDto)
    {
        var addOn = await _restaurantService.CreateAddOnAsync(createDto);
        return CreatedAtAction(nameof(GetAddOnById), new { id = addOn.Id }, addOn);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAddOn(Guid id, [FromBody] UpdateAddOnDto updateDto)
    {
        await _restaurantService.UpdateAddOnAsync(id, updateDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAddOn(Guid id)
    {
        await _restaurantService.DeleteAddOnAsync(id);
        return NoContent();
    }
}
