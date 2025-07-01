namespace FoodOrderingSystem.Rider.Domain.Entities;

public class Rider
{
    public Guid Id { get; set; } // Corresponds to a User in the Auth service
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string VehicleDetails { get; set; } // e.g., "Motorcycle - ABC 123"
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public virtual RiderStatus Status { get; set; }
    public virtual ICollection<RiderOrder> RiderOrders { get; set; } = new List<RiderOrder>();
    public virtual ICollection<LocationLog> LocationLogs { get; set; } = new List<LocationLog>();
} 