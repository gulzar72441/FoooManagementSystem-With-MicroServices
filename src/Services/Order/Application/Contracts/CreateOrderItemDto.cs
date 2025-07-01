namespace FoodOrderingSystem.Order.Application.Contracts;

public class CreateOrderItemDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
} 