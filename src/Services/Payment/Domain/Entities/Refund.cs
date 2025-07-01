namespace FoodOrderingSystem.Payment.Domain.Entities;

public class Refund
{
    public Guid Id { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string? Reason { get; set; }
    public DateTime RefundDate { get; set; }
    public virtual FoodOrderingSystem.Payment.Domain.Entities.Payment? Payment { get; set; }
} 