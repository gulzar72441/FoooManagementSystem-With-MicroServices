namespace FoodOrderingSystem.Payment.Application.Contracts;

public class CreateRefundDto
{
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string? Reason { get; set; }
}
