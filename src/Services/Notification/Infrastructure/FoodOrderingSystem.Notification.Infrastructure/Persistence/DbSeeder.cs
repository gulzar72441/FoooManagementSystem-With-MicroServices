using Bogus;
using FoodOrderingSystem.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Notification.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(NotificationDbContext context)
    {
        if (await context.Notifications.AnyAsync())
        {
            return; // DB has been seeded
        }

        var faker = new Faker<Domain.Entities.Notification>()
            .RuleFor(n => n.Id, f => f.Random.Guid())
            .RuleFor(n => n.UserId, f => f.Internet.Email())
            .RuleFor(n => n.Message, f => f.Lorem.Sentence())
            .RuleFor(n => n.Timestamp, f => f.Date.Past())
            .RuleFor(n => n.IsRead, f => f.Random.Bool());

        var notifications = faker.Generate(20);

        await context.Notifications.AddRangeAsync(notifications);
        await context.SaveChangesAsync();
    }
}
