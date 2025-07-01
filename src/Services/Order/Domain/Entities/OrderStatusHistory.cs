namespace FoodOrderingSystem.Order.Domain.Entities;

public class OrderStatusHistory
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public string Status { get; set; }
    public DateTime ChangeDate { get; set; }
    public string Notes { get; set; }
    public virtual Order Order { get; set; }
} 
 