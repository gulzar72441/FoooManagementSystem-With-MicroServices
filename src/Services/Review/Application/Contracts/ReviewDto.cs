namespace FoodOrderingSystem.Review.Application.Contracts;

public class ReviewDto
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid CustomerId { get; set; }
    public int Rating { get; set; } // e.g., 1-5
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
} 