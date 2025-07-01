namespace FoodOrderingSystem.Restaurant.Application.Contracts;

public class MenuDto
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string Name { get; set; }
    public List<MenuItemDto> Items { get; set; } = new();
} 