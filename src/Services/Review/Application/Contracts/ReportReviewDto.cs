namespace FoodOrderingSystem.Review.Application.Contracts;

public class ReportReviewDto
{
    public Guid ReviewId { get; set; }
    public Guid ReportedByUserId { get; set; }
    public string Reason { get; set; }
} 