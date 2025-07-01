using FoodOrderingSystem.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Notification.Infrastructure.Persistence;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Notification> Notifications { get; set; }
}
