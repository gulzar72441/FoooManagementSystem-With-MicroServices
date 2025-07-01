namespace FoodOrderingSystem.Payment.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string? Type { get; set; } // "Payment" or "Refund"
    public DateTime TransactionDate { get; set; }
    public string? ExternalTransactionId { get; set; }
    public virtual FoodOrderingSystem.Payment.Domain.Entities.Payment? Payment { get; set; }
} 