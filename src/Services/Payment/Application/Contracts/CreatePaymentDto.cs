namespace FoodOrderingSystem.Payment.Application.Contracts;

public class CreatePaymentDto
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string? PaymentMethod { get; set; }
    public string? CardNumber { get; set; } // Simplified for example
    public string? ExpiryDate { get; set; } // Simplified for example
    public string? Cvv { get; set; } // Simplified for example
} 