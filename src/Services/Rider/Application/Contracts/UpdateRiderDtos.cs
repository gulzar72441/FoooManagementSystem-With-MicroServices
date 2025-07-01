namespace FoodOrderingSystem.Rider.Application.Contracts;

public class UpdateRiderLocationDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class UpdateRiderStatusDto
{
    public string Status { get; set; }
} 