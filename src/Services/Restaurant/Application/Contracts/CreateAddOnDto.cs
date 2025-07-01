namespace FoodOrderingSystem.Restaurant.Application.Contracts;

public class CreateAddOnDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
