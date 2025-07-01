namespace FoodOrderingSystem.Restaurant.Domain.Entities;

public class AddOn
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public Guid MenuItemId { get; set; }
    public virtual MenuItem MenuItem { get; set; } = null!;
} 