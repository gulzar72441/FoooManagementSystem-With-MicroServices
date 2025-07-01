namespace FoodOrderingSystem.Payment.Application.Contracts;

public class TransactionDto
{
    public Guid Id { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
    public string? Type { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? ExternalTransactionId { get; set; }
}
