using FoodOrderingSystem.Notification.Application.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace FoodOrderingSystem.Notification.API.Hubs;

public class NotificationHub : Hub<INotificationClient>
{
    public async Task SendNotification(string user, string message)
    {
        await Clients.All.ReceiveNotification(user, message);
    }
} 