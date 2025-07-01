namespace FoodOrderingSystem.Rider.Domain.Entities;

public class RiderStatus
{
    public Guid Id { get; set; }
    public Guid RiderId { get; set; }
    public bool IsOnline { get; set; }
    public string Status { get; set; } // e.g., "Available", "OnDelivery", "Offline"
    public DateTime LastUpdated { get; set; }
    public virtual Rider Rider { get; set; }
} 