namespace FoodOrderingSystem.Restaurant.Application.Contracts;

public class CreateMenuItemDto
{
    public Guid MenuId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
} 