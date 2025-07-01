using FoodOrderingSystem.Order.Application.Contracts;
using FoodOrderingSystem.Order.Application.Services;
using FoodOrderingSystem.Order.Domain.Entities;
using FoodOrderingSystem.Order.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Order.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly OrderDbContext _context;

    public OrderService(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
    {
        var order = new Domain.Entities.Order
        {
            CustomerId = dto.CustomerId,
            RestaurantId = dto.RestaurantId,
            OrderDate = DateTime.UtcNow,
            Status = "Pending",
            DeliveryAddress = dto.DeliveryAddress,
            TotalAmount = dto.Items.Sum(i => i.Price * i.Quantity),
            OrderItems = dto.Items.Select(i => new OrderItem
            {
                MenuItemId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.Price
            }).ToList()
        };

        _context.Orders.Add(order);
        
        var history = new OrderStatusHistory
        {
            Order = order,
            Status = order.Status,
            ChangeDate = DateTime.UtcNow,
            Notes = "Order created."
        };
        _context.OrderStatusHistories.Add(history);
        
        await _context.SaveChangesAsync();

        return await GetOrderByIdAsync(order.Id);
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.Id == orderId)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                RestaurantId = o.RestaurantId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                DeliveryAddress = o.DeliveryAddress,
                Items = o.OrderItems.Select(i => new OrderItemDto
                {
                    ProductId = i.MenuItemId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.UnitPrice
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return order;
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.CustomerId == customerId)
            .Select(o => new OrderDto 
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                RestaurantId = o.RestaurantId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                DeliveryAddress = o.DeliveryAddress,
                Items = o.OrderItems.Select(i => new OrderItemDto
                {
                    ProductId = i.MenuItemId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.UnitPrice
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByRestaurantIdAsync(Guid restaurantId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.RestaurantId == restaurantId)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                RestaurantId = o.RestaurantId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                DeliveryAddress = o.DeliveryAddress,
                Items = o.OrderItems.Select(i => new OrderItemDto
                {
                    ProductId = i.MenuItemId,
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.UnitPrice
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<OrderDto> UpdateOrderStatusAsync(Guid orderId, UpdateOrderStatusDto dto)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) throw new Exception("Order not found");

        order.Status = dto.Status;
        
        var history = new OrderStatusHistory
        {
            OrderId = orderId,
            Status = dto.Status,
            ChangeDate = DateTime.UtcNow,
            Notes = dto.Notes
        };
        _context.OrderStatusHistories.Add(history);

        await _context.SaveChangesAsync();
        
        return await GetOrderByIdAsync(orderId);
    }
} 