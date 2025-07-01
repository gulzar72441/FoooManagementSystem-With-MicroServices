namespace FoodOrderingSystem.Restaurant.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public Guid MenuId { get; set; }
    public virtual Menu Menu { get; set; } = null!;
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<AddOn> AddOns { get; set; } = new List<AddOn>();
} 