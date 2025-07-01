namespace FoodOrderingSystem.Payment.Domain.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; } // "Pending", "Completed", "Failed"
    public DateTime PaymentDate { get; set; }
    public string? PaymentMethod { get; set; } // "CreditCard", "PayPal", etc.
} 