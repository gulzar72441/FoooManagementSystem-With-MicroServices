namespace FoodOrderingSystem.Payment.Domain.Entities;

public class Commission
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal RestaurantCommission { get; set; }
    public decimal RiderCommission { get; set; }
    public DateTime CalculationDate { get; set; }
} 