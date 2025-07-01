namespace FoodOrderingSystem.Review.Domain.Entities;

public class Rating
{
    public Guid Id { get; set; }
    public Guid EntityId { get; set; } // RestaurantId or RiderId
    public string EntityType { get; set; } // "Restaurant" or "Rider"
    public int Score { get; set; } // e.g., 1-5
    public Guid CustomerId { get; set; }
    public DateTime RatingDate { get; set; }
    public Guid ReviewId { get; set; }
} 