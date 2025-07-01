using FoodOrderingSystem.Restaurant.Application.Contracts;
using FoodOrderingSystem.Restaurant.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Restaurant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants()
    {
        var restaurants = await _restaurantService.GetAllRestaurantsAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRestaurantById(Guid id)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
        if (restaurant == null) return NotFound();
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createDto)
    {
        var restaurant = await _restaurantService.CreateRestaurantAsync(createDto);
        return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRestaurant(Guid id, [FromBody] UpdateRestaurantDto updateDto)
    {
        await _restaurantService.UpdateRestaurantAsync(id, updateDto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRestaurant(Guid id)
    {
        await _restaurantService.DeleteRestaurantAsync(id);
        return NoContent();
    }

    [HttpGet("{id:guid}/menu")]
    public async Task<IActionResult> GetMenu(Guid id)
    {
        var menu = await _restaurantService.GetMenusByRestaurantIdAsync(id);
        if (menu == null) return NotFound();
        return Ok(menu);
    }
} 