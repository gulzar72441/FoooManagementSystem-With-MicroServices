using FoodOrderingSystem.Rider.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Rider.Infrastructure.Persistence;

public class RiderDbContext : DbContext
{
    public RiderDbContext(DbContextOptions<RiderDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Rider> Riders { get; set; }
    public DbSet<RiderStatus> RiderStatuses { get; set; }
    public DbSet<RiderOrder> RiderOrders { get; set; }
    public DbSet<LocationLog> LocationLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Entities.Rider>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
            entity.Property(e => e.VehicleDetails).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<RiderStatus>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Rider)
                .WithOne(r => r.Status)
                .HasForeignKey<RiderStatus>(e => e.RiderId);
        });

        modelBuilder.Entity<RiderOrder>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Rider)
                .WithMany(r => r.RiderOrders)
                .HasForeignKey(e => e.RiderId);
        });

        modelBuilder.Entity<LocationLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Rider)
                .WithMany(r => r.LocationLogs)
                .HasForeignKey(e => e.RiderId);
        });
    }
} 