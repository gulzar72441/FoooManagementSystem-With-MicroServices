using Bogus;
using FoodOrderingSystem.Review.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Review.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(ReviewDbContext context)
    {
        if (await context.Reviews.AnyAsync()) return;

        var entityTypes = new[] { "Restaurant", "Rider" };
        var reviewFaker = new Faker<Domain.Entities.Review>()
            .RuleFor(r => r.Id, f => f.Random.Guid())
            .RuleFor(r => r.EntityId, f => f.Random.Guid())
            .RuleFor(r => r.EntityType, f => f.PickRandom(entityTypes))
            .RuleFor(r => r.CustomerId, f => f.Random.Guid())
            .RuleFor(r => r.Comment, f => f.Lorem.Sentence())
            .RuleFor(r => r.CreatedAt, f => f.Date.Past(2))
            .RuleFor(r => r.IsAbusive, f => f.Random.Bool(0.1f));

        var reviews = reviewFaker.Generate(20);
        await context.Reviews.AddRangeAsync(reviews);
        await context.SaveChangesAsync();
    }
}