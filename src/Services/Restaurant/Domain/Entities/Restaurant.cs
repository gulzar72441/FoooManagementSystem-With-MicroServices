namespace FoodOrderingSystem.Restaurant.Domain.Entities;

public class Restaurant
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string CuisineType { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
} 