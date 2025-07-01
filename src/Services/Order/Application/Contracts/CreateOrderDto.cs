namespace FoodOrderingSystem.Order.Application.Contracts;

public class CreateOrderDto
{
    public Guid CustomerId { get; set; }
    public Guid RestaurantId { get; set; }
    public string DeliveryAddress { get; set; }
    public List<CreateOrderItemDto> Items { get; set; }
} 