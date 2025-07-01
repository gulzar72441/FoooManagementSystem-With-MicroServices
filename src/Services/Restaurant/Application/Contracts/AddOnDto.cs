namespace FoodOrderingSystem.Restaurant.Application.Contracts;

public class AddOnDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
} 