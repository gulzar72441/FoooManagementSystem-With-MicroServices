namespace FoodOrderingSystem.Payment.Application.Contracts;

public class CommissionDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal RestaurantCommission { get; set; }
    public decimal RiderCommission { get; set; }
    public DateTime CalculationDate { get; set; }
}
