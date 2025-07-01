namespace FoodOrderingSystem.Rider.Domain.Entities;

public class LocationLog
{
    public Guid Id { get; set; }
    public Guid RiderId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Timestamp { get; set; }
    public virtual Rider Rider { get; set; }
} 