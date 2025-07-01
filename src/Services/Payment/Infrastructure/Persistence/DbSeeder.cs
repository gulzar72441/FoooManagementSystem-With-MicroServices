using Bogus;
using FoodOrderingSystem.Payment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Payment.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(PaymentDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Payments.AnyAsync()) return; 

        var paymentStatus = new[] { "Pending", "Completed", "Failed" };
        var paymentMethods = new[] { "CreditCard", "PayPal", "Cash" };

        var payments = new Faker<Domain.Entities.Payment>()
            .RuleFor(p => p.Id, f => f.Random.Guid())
            .RuleFor(p => p.OrderId, f => f.Random.Guid())
            .RuleFor(p => p.Amount, f => f.Finance.Amount(5, 100))
            .RuleFor(p => p.Status, f => f.PickRandom(paymentStatus))
            .RuleFor(p => p.PaymentDate, f => f.Date.Past(2))
            .RuleFor(p => p.PaymentMethod, f => f.PickRandom(paymentMethods))
            .Generate(20);

        await context.Payments.AddRangeAsync(payments);

        var transactions = new List<Transaction>();
        foreach (var payment in payments)
        {
            if (payment.Status == "Completed")
            {
                var transaction = new Faker<Transaction>()
                    .RuleFor(t => t.Id, f => f.Random.Guid())
                    .RuleFor(t => t.PaymentId, payment.Id)
                    .RuleFor(t => t.Amount, payment.Amount)
                    .RuleFor(t => t.Type, "Payment")
                    .RuleFor(t => t.TransactionDate, payment.PaymentDate)
                    .RuleFor(t => t.ExternalTransactionId, f => f.Random.AlphaNumeric(16))
                    .Generate();
                transactions.Add(transaction);
            }
        }

        await context.Transactions.AddRangeAsync(transactions);

        var commissions = new Faker<Commission>()
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.OrderId, f => f.Random.Guid())
            .RuleFor(c => c.TotalAmount, f => f.Finance.Amount(10, 150))
            .RuleFor(c => c.RestaurantCommission, (f, c) => c.TotalAmount * 0.1m)
            .RuleFor(c => c.RiderCommission, (f, c) => c.TotalAmount * 0.05m)
            .RuleFor(c => c.CalculationDate, f => f.Date.Recent())
            .Generate(15);

        await context.Commissions.AddRangeAsync(commissions);

        await context.SaveChangesAsync();
    }
} 