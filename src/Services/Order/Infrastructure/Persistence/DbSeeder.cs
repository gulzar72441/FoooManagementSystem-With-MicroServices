using Bogus;
using FoodOrderingSystem.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Order.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(OrderDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Orders.AnyAsync())
        {
            return;
        }

        var allOrderItems = new List<OrderItem>();
        var allStatusHistories = new List<OrderStatusHistory>();
        var orderStatus = new[] { "Pending", "Accepted", "Preparing", "Picked", "Delivered" };

        var orderFaker = new Faker<Domain.Entities.Order>()
            .RuleFor(o => o.Id, f => Guid.NewGuid())
            .RuleFor(o => o.CustomerId, f => Guid.NewGuid()) // In a real app, this would come from the Auth service
            .RuleFor(o => o.RestaurantId, f => Guid.NewGuid()) // In a real app, this would come from the Restaurant service
            .RuleFor(o => o.OrderDate, f => f.Date.Past(1))
            .RuleFor(o => o.TotalAmount, f => f.Random.Decimal(20, 200))
            .RuleFor(o => o.Status, f => f.PickRandom(orderStatus))
            .RuleFor(o => o.DeliveryAddress, f => f.Address.FullAddress());

        var orders = orderFaker.Generate(20);

        var random = new Faker();

        foreach (var order in orders)
        {
            var orderItemFaker = new Faker<OrderItem>()
                .RuleFor(oi => oi.Id, f => Guid.NewGuid())
                .RuleFor(oi => oi.OrderId, order.Id)
                .RuleFor(oi => oi.MenuItemId, f => Guid.NewGuid()) // In a real app, this would come from the Restaurant service
                .RuleFor(oi => oi.ProductName, f => f.Commerce.ProductName())
                .RuleFor(oi => oi.Quantity, f => f.Random.Int(1, 5))
                .RuleFor(oi => oi.UnitPrice, f => f.Random.Decimal(5, 50));

            var orderItems = orderItemFaker.Generate(random.Random.Int(1, 5));
            allOrderItems.AddRange(orderItems);

            var statusHistoryFaker = new Faker<OrderStatusHistory>()
                .RuleFor(sh => sh.Id, f => Guid.NewGuid())
                .RuleFor(sh => sh.OrderId, order.Id)
                .RuleFor(sh => sh.Status, f => f.PickRandom(orderStatus))
                .RuleFor(sh => sh.ChangeDate, f => f.Date.Recent())
                .RuleFor(sh => sh.Notes, f => f.Lorem.Sentence());

            var statusHistories = statusHistoryFaker.Generate(random.Random.Int(1, 3));
            allStatusHistories.AddRange(statusHistories);
        }

        await context.Orders.AddRangeAsync(orders);
        await context.OrderItems.AddRangeAsync(allOrderItems);
        await context.OrderStatusHistories.AddRangeAsync(allStatusHistories);

        await context.SaveChangesAsync();
    }
} 