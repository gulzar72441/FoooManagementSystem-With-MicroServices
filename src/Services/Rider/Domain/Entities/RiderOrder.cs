namespace FoodOrderingSystem.Rider.Domain.Entities;

public class RiderOrder
{
    public Guid Id { get; set; }
    public Guid RiderId { get; set; }
    public Guid OrderId { get; set; } // From the Order service
    public string Status { get; set; } // e.g., "Assigned", "PickedUp", "Delivered"
    public DateTime AssignmentDate { get; set; }
    public virtual Rider Rider { get; set; }
} 