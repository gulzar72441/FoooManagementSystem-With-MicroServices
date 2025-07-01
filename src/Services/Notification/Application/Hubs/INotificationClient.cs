namespace FoodOrderingSystem.Notification.Application.Hubs;

public interface INotificationClient
{
    Task ReceiveNotification(string user, string message);
} 