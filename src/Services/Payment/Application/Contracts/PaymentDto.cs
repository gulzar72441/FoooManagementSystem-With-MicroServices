namespace FoodOrderingSystem.Payment.Application.Contracts;

public class PaymentDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public string? PaymentMethod { get; set; }
} 