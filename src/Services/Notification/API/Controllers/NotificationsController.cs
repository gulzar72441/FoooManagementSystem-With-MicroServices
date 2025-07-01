using FoodOrderingSystem.Notification.Application.Hubs;
using FoodOrderingSystem.Notification.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FoodOrderingSystem.Notification.API.Controllers;

public class NotificationDto
{
    public string User { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}


[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly IHubContext<Hubs.NotificationHub, INotificationClient> _hubContext;
    private readonly NotificationDbContext _dbContext;

    public NotificationsController(IHubContext<Hubs.NotificationHub, INotificationClient> hubContext, NotificationDbContext dbContext)
    {
        _hubContext = hubContext;
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] NotificationDto notification)
    {
        var newNotification = new Domain.Entities.Notification
        {
            UserId = notification.User,
            Message = notification.Message,
            Timestamp = DateTime.UtcNow,
            IsRead = false
        };

        _dbContext.Notifications.Add(newNotification);
        await _dbContext.SaveChangesAsync();

        await _hubContext.Clients.All.ReceiveNotification(notification.User, notification.Message);
        return Ok();
    }
} 