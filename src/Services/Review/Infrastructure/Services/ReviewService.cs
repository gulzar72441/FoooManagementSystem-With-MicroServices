using FoodOrderingSystem.Review.Application.Contracts;
using FoodOrderingSystem.Review.Application.Services;
using FoodOrderingSystem.Review.Domain.Entities;
using FoodOrderingSystem.Review.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Review.Infrastructure.Services;

public class ReviewService : IReviewService
{
    private readonly ReviewDbContext _context;

    public ReviewService(ReviewDbContext context)
    {
        _context = context;
    }

    public async Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto)
    {
        var review = new Domain.Entities.Review
        {
            Id = Guid.NewGuid(),
            RestaurantId = dto.RestaurantId,
            CustomerId = dto.CustomerId,
            Comment = dto.Comment,
            CreatedAt = DateTime.UtcNow
        };

        var rating = new Rating
        {
            Id = Guid.NewGuid(),
            ReviewId = review.Id,
            Score = dto.Rating
        };
        
        await _context.Reviews.AddAsync(review);
        await _context.Ratings.AddAsync(rating);

        await _context.SaveChangesAsync();
        
        // This is a simplification. In reality, you'd update an aggregate rating.
        
        return MapToReviewDto(review, rating.Score);
    }

    public async Task<ReviewDto> GetReviewByIdAsync(Guid reviewId)
    {
        var review = await _context.Reviews.FindAsync(reviewId);
        if (review == null) return null;

        var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.ReviewId == reviewId);
        
        return MapToReviewDto(review, rating?.Score ?? 0);
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsByRestaurantAsync(Guid restaurantId)
    {
        return await _context.Reviews
            .Where(r => r.RestaurantId == restaurantId)
            .Select(r => new ReviewDto
            {
                Id = r.Id,
                RestaurantId = r.RestaurantId,
                CustomerId = r.CustomerId,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt,
                Rating = _context.Ratings.FirstOrDefault(rt => rt.ReviewId == r.Id).Score
            }).ToListAsync();
    }

    public async Task ReportReviewAsync(ReportReviewDto dto)
    {
        var report = new ReportedReview
        {
            Id = Guid.NewGuid(),
            ReviewId = dto.ReviewId,
            ReportedByUserId = dto.ReportedByUserId,
            Reason = dto.Reason,
            ReportedAt = DateTime.UtcNow
        };

        await _context.ReportedReviews.AddAsync(report);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReviewAsync(Guid reviewId)
    {
        var review = await _context.Reviews.FindAsync(reviewId);
        if(review == null) return;
        
        var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.ReviewId == reviewId);
        if(rating != null) _context.Ratings.Remove(rating);

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
    }
    
    private static ReviewDto MapToReviewDto(Domain.Entities.Review review, int rating)
    {
        return new ReviewDto
        {
            Id = review.Id,
            RestaurantId = review.RestaurantId,
            CustomerId = review.CustomerId,
            Rating = rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt
        };
    }
} 