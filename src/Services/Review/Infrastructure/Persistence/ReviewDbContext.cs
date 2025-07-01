using FoodOrderingSystem.Review.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Review.Infrastructure.Persistence;

public class ReviewDbContext : DbContext
{
    public ReviewDbContext(DbContextOptions<ReviewDbContext> options) : base(options)
    {
    }

    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Domain.Entities.Review> Reviews { get; set; }
    public DbSet<ReportedReview> ReportedReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EntityType).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Domain.Entities.Review>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EntityType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Comment).IsRequired();
        });

        modelBuilder.Entity<ReportedReview>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Reason).IsRequired().HasMaxLength(250);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Review)
                .WithMany()
                .HasForeignKey(e => e.ReviewId);
        });
    }
} 