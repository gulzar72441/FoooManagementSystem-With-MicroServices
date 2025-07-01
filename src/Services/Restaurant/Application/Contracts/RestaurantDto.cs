namespace FoodOrderingSystem.Restaurant.Application.Contracts;

public class RestaurantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string CuisineType { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
} 