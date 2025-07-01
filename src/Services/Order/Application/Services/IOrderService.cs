using FoodOrderingSystem.Order.Application.Contracts;

namespace FoodOrderingSystem.Order.Application.Services;

public interface IOrderService
{
    Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
    Task<OrderDto> GetOrderByIdAsync(Guid orderId);
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<OrderDto>> GetOrdersByRestaurantIdAsync(Guid restaurantId);
    Task<OrderDto> UpdateOrderStatusAsync(Guid orderId, UpdateOrderStatusDto dto);
} 