using FoodOrderingSystem.Restaurant.Application.Contracts;
using FoodOrderingSystem.Restaurant.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Restaurant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public MenuItemsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMenuItemById(Guid id)
    {
        var item = await _restaurantService.GetMenuItemByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> AddMenuItem([FromBody] CreateMenuItemDto createDto)
    {
        var menuItem = await _restaurantService.AddMenuItemAsync(createDto);
        return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItem.Id }, menuItem);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMenuItem(Guid id, [FromBody] UpdateMenuItemDto updateDto)
    {
        try
        {
            await _restaurantService.UpdateMenuItemAsync(id, updateDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMenuItem(Guid id)
    {
        try
        {
            await _restaurantService.DeleteMenuItemAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
} 