namespace FoodOrderingSystem.Rider.Application.Contracts;

public class CreateRiderDto
{
    public Guid Id { get; set; } // This should match the User ID from Auth Service
    public string Name { get; set; }
    public string VehicleDetails { get; set; }
} 