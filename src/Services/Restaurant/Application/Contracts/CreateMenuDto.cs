namespace FoodOrderingSystem.Restaurant.Application.Contracts;

public class CreateMenuDto
{
    public string Name { get; set; } = string.Empty;
    public Guid RestaurantId { get; set; }
}
