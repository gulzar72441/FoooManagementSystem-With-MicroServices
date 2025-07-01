namespace FoodOrderingSystem.Review.Domain.Entities;

public class ReportedReview
{
    public Guid Id { get; set; }
    public Guid ReviewId { get; set; }
    public Guid ReportedByUserId { get; set; }
    public string Reason { get; set; }
    public DateTime ReportedAt { get; set; }
    public string Status { get; set; } // "Pending", "Reviewed", "Dismissed"
    public virtual Review Review { get; set; }
} 