namespace FoodOrderingSystem.Restaurant.Domain.Entities;

public class Menu
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; } = null!;
    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}