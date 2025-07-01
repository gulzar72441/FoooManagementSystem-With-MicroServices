using FoodOrderingSystem.Rider.Application.Contracts;

namespace FoodOrderingSystem.Rider.Application.Services;

public interface IRiderService
{
    Task<RiderDto> CreateRiderAsync(CreateRiderDto dto);
    Task<RiderDto> GetRiderByIdAsync(Guid riderId);
    Task<IEnumerable<RiderDto>> GetAvailableRidersAsync();
    Task UpdateRiderLocationAsync(Guid riderId, UpdateRiderLocationDto dto);
    Task UpdateRiderStatusAsync(Guid riderId, UpdateRiderStatusDto dto);
    Task AssignOrderToRiderAsync(Guid riderId, AssignOrderDto dto);
    Task CompleteOrderAsync(Guid riderId, Guid orderId);
} 