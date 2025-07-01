namespace FoodOrderingSystem.Rider.Application.Contracts;

public class RiderDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string VehicleDetails { get; set; }
    public string CurrentStatus { get; set; } // e.g., "Available", "On-Delivery"
    public double Latitude { get; set; }
    public double Longitude { get; set; }
} 