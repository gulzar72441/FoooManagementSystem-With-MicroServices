using FoodOrderingSystem.Review.Application.Contracts;

namespace FoodOrderingSystem.Review.Application.Services;

public interface IReviewService
{
    Task<ReviewDto> GetReviewByIdAsync(Guid reviewId);
    Task<ReviewDto> CreateReviewAsync(CreateReviewDto dto);
    Task<IEnumerable<ReviewDto>> GetReviewsByRestaurantAsync(Guid restaurantId);
    Task ReportReviewAsync(ReportReviewDto dto);
    Task DeleteReviewAsync(Guid reviewId);
} 