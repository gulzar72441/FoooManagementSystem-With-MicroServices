namespace FoodOrderingSystem.Restaurant.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
} 