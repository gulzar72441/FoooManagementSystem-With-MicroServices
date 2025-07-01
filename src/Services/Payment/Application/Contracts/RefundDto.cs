namespace FoodOrderingSystem.Payment.Application.Contracts;

public class RefundDto
{
    public Guid Id { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string? Reason { get; set; }
    public DateTime RefundDate { get; set; }
}
