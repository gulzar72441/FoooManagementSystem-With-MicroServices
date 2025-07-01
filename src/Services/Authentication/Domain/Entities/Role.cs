namespace FoodOrderingSystem.Authentication.Domain.Entities;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } // "Customer", "RestaurantOwner", "Rider", "Admin"
    public virtual ICollection<User> Users { get; set; } = new List<User>();
} 