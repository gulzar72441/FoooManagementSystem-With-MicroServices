namespace FoodOrderingSystem.Restaurant.Application.Contracts;

public class UpdateAddOnDto
{
    public string Name { get; set; } = string.Empty;
    public decimal? Price { get; set; }
}
