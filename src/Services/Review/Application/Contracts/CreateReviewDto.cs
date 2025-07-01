namespace FoodOrderingSystem.Review.Application.Contracts;

public class CreateReviewDto
{
    public Guid RestaurantId { get; set; }
    public Guid CustomerId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
} 