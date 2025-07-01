using FoodOrderingSystem.Review.Application.Contracts;
using FoodOrderingSystem.Review.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Review.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
    {
        var review = await _reviewService.CreateReviewAsync(dto);
        return CreatedAtAction(nameof(GetReviewById), new { reviewId = review.Id }, review);
    }

    [HttpGet("{reviewId}", Name = "GetReviewById")]
    public async Task<IActionResult> GetReviewById(Guid reviewId)
    {
        var review = await _reviewService.GetReviewByIdAsync(reviewId);
        return review == null ? NotFound() : Ok(review);
    }

    [HttpGet("restaurant/{restaurantId}")]
    public async Task<IActionResult> GetReviewsByRestaurant(Guid restaurantId)
    {
        var reviews = await _reviewService.GetReviewsByRestaurantAsync(restaurantId);
        return Ok(reviews);
    }

    [HttpPost("report")]
    public async Task<IActionResult> ReportReview([FromBody] ReportReviewDto dto)
    {
        await _reviewService.ReportReviewAsync(dto);
        return NoContent();
    }

    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(Guid reviewId)
    {
        await _reviewService.DeleteReviewAsync(reviewId);
        return NoContent();
    }
} 