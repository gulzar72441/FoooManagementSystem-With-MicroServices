namespace FoodOrderingSystem.Restaurant.Application.Contracts;

public class UpdateRestaurantDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string CuisineType { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
