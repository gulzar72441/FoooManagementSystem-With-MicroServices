namespace FoodOrderingSystem.Review.Domain.Entities;

public class Review
{
    public Guid Id { get; set; }
    public Guid EntityId { get; set; } // RestaurantId or RiderId
    public string EntityType { get; set; } // "Restaurant" or "Rider"
    public Guid CustomerId { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsAbusive { get; set; }
    public Guid RestaurantId { get; set; }
} 