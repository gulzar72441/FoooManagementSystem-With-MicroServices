using FoodOrderingSystem.Restaurant.Application.Contracts;
using FoodOrderingSystem.Restaurant.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Restaurant.API.Controllers;

[ApiController]
[Route("api/menus")]
public class MenusController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public MenusController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMenuById(Guid id)
    {
        var menu = await _restaurantService.GetMenuByIdAsync(id);
        if (menu == null) return NotFound();
        return Ok(menu);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenu([FromBody] CreateMenuDto createDto)
    {
        var menu = await _restaurantService.CreateMenuAsync(createDto);
        return CreatedAtAction(nameof(GetMenuById), new { id = menu.Id }, menu);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMenu(Guid id, [FromBody] UpdateMenuDto updateDto)
    {
        await _restaurantService.UpdateMenuAsync(id, updateDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMenu(Guid id)
    {
        await _restaurantService.DeleteMenuAsync(id);
        return NoContent();
    }

    [HttpGet("{id:guid}/menuitems")]
    public async Task<IActionResult> GetMenuItemsByMenuId(Guid id)
    {
        var items = await _restaurantService.GetMenuItemsByMenuIdAsync(id);
        return Ok(items);
    }
}
