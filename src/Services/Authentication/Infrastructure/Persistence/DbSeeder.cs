using Bogus;
using FoodOrderingSystem.Authentication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Authentication.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AuthDbContext context)
    {
        // No need to migrate for in-memory database
        if (!await context.Roles.AnyAsync())
        {
            var roles = new List<Role>
            {
                new() { Id = Guid.NewGuid(), Name = "Admin" },
                new() { Id = Guid.NewGuid(), Name = "RestaurantOwner" },
                new() { Id = Guid.NewGuid(), Name = "Customer" },
                new() { Id = Guid.NewGuid(), Name = "Rider" }
            };
            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var passwordHasher = new PasswordHasher<User>();
            var roles = await context.Roles.ToListAsync();

            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Address, f => f.Address.FullAddress())
                .RuleFor(u => u.ImageUrl, f => f.Internet.Avatar())
                .RuleFor(u => u.RoleId, f => f.PickRandom(roles).Id);

            var users = userFaker.Generate(20);

            foreach (var user in users)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, "Password123!");
            }

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
} 