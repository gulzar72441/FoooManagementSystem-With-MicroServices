using Bogus;
using FoodOrderingSystem.Rider.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Rider.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(RiderDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Riders.AnyAsync())
        {
            return;
        }

        var riders = new List<Domain.Entities.Rider>();
        var random = new Faker();

        for (int i = 0; i < 15; i++)
        {
            var isOnline = random.Random.Bool();
            var riderStatus = new Faker<RiderStatus>()
                .RuleFor(s => s.Id, f => Guid.NewGuid())
                .RuleFor(s => s.IsOnline, isOnline)
                .RuleFor(s => s.Status, f => isOnline ? f.PickRandom("Available", "OnDelivery") : "Offline")
                .RuleFor(s => s.LastUpdated, f => f.Date.Recent())
                .Generate();

            var rider = new Faker<Domain.Entities.Rider>()
                .RuleFor(r => r.Id, f => Guid.NewGuid()) // Corresponds to a user in Auth service
                .RuleFor(r => r.Name, f => f.Name.FullName())
                .RuleFor(r => r.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(r => r.VehicleDetails, f => $"Motorcycle - {f.Vehicle.Model()}")
                .RuleFor(r => r.Status, riderStatus)
                .Generate();

            var orders = new Faker<RiderOrder>()
                .RuleFor(o => o.Id, f => Guid.NewGuid())
                .RuleFor(o => o.RiderId, rider.Id)
                .RuleFor(o => o.OrderId, f => Guid.NewGuid()) // From Order service
                .RuleFor(o => o.Status, f => f.PickRandom("Assigned", "PickedUp", "Delivered"))
                .RuleFor(o => o.AssignmentDate, f => f.Date.Past(1))
                .Generate(random.Random.Int(1, 10));
            
            rider.RiderOrders = orders;

            var locations = new Faker<LocationLog>()
                .RuleFor(l => l.Id, f => Guid.NewGuid())
                .RuleFor(l => l.RiderId, rider.Id)
                .RuleFor(l => l.Latitude, f => f.Address.Latitude())
                .RuleFor(l => l.Longitude, f => f.Address.Longitude())
                .RuleFor(l => l.Timestamp, f => f.Date.Recent())
                .Generate(random.Random.Int(10, 50));
            
            rider.LocationLogs = locations;
            
            riders.Add(rider);
        }

        await context.Riders.AddRangeAsync(riders);
        await context.SaveChangesAsync();
    }
} 