using FoodOrderingSystem.Rider.Application.Contracts;
using FoodOrderingSystem.Rider.Application.Services;
using FoodOrderingSystem.Rider.Domain.Entities;
using FoodOrderingSystem.Rider.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Rider.Infrastructure.Services;

public class RiderService : IRiderService
{
    private readonly RiderDbContext _context;

    public RiderService(RiderDbContext context)
    {
        _context = context;
    }

    public async Task<RiderDto> CreateRiderAsync(CreateRiderDto dto)
    {
        var riderStatus = await _context.RiderStatuses.FirstOrDefaultAsync(s => s.Status == "Available");
        if (riderStatus == null)
        {
            riderStatus = new RiderStatus { Status = "Available" };
            _context.RiderStatuses.Add(riderStatus);
        }
        
        var rider = new Domain.Entities.Rider
        {
            Id = dto.Id,
            Name = dto.Name,
            VehicleDetails = dto.VehicleDetails,
            Status = riderStatus,
            Latitude = 0, // Default location
            Longitude = 0
        };

        await _context.Riders.AddAsync(rider);
        await _context.SaveChangesAsync();

        return MapToRiderDto(rider);
    }
    
    public async Task<IEnumerable<RiderDto>> GetAvailableRidersAsync()
    {
        return await _context.Riders
            .Where(r => r.Status.Status == "Available")
            .Select(r => MapToRiderDto(r))
            .ToListAsync();
    }

    public async Task<RiderDto> GetRiderByIdAsync(Guid riderId)
    {
        var rider = await _context.Riders.Include(r => r.Status).FirstOrDefaultAsync(r => r.Id == riderId);
        return rider == null ? null : MapToRiderDto(rider);
    }

    public async Task UpdateRiderLocationAsync(Guid riderId, UpdateRiderLocationDto dto)
    {
        var rider = await _context.Riders.FindAsync(riderId);
        if (rider == null) throw new Exception("Rider not found");

        rider.Latitude = dto.Latitude;
        rider.Longitude = dto.Longitude;

        _context.LocationLogs.Add(new LocationLog { RiderId = riderId, Latitude = dto.Latitude, Longitude = dto.Longitude, Timestamp = DateTime.UtcNow });
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRiderStatusAsync(Guid riderId, UpdateRiderStatusDto dto)
    {
        var rider = await _context.Riders
            .Include(r => r.Status)
            .FirstOrDefaultAsync(r => r.Id == riderId);

        if (rider == null)
        {
            throw new Exception("Rider not found");
        }

        if (rider.Status == null)
        {
            rider.Status = new RiderStatus
            {
                RiderId = riderId,
                Status = dto.Status,
                IsOnline = dto.Status != "Offline",
                LastUpdated = DateTime.UtcNow
            };
        }
        else
        {
            rider.Status.Status = dto.Status;
            rider.Status.IsOnline = dto.Status != "Offline";
            rider.Status.LastUpdated = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }

    public async Task AssignOrderToRiderAsync(Guid riderId, AssignOrderDto dto)
    {
        var rider = await _context.Riders.FindAsync(riderId);
        if (rider == null) throw new Exception("Rider not found");

        _context.RiderOrders.Add(new RiderOrder { RiderId = riderId, OrderId = dto.OrderId, AssignmentDate = DateTime.UtcNow });

        await UpdateRiderStatusAsync(riderId, new UpdateRiderStatusDto { Status = "On-Delivery" });
        // Note: No SaveChangesAsync here because UpdateRiderStatusAsync already calls it.
    }

    public async Task CompleteOrderAsync(Guid riderId, Guid orderId)
    {
        var riderOrder = await _context.RiderOrders.FirstOrDefaultAsync(ro => ro.RiderId == riderId && ro.OrderId == orderId);
        if (riderOrder != null)
        {
            _context.RiderOrders.Remove(riderOrder);
        }
        await UpdateRiderStatusAsync(riderId, new UpdateRiderStatusDto { Status = "Available" });
    }

    private static RiderDto MapToRiderDto(Domain.Entities.Rider rider)
    {
        return new RiderDto
        {
            Id = rider.Id,
            Name = rider.Name,
            VehicleDetails = rider.VehicleDetails,
            CurrentStatus = rider.Status?.Status ?? "Offline",
            Latitude = rider.Latitude ?? 0.0,
            Longitude = rider.Longitude ?? 0.0
        };
    }
} 