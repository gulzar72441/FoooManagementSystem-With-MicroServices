namespace FoodOrderingSystem.Authentication.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PasswordHash { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    public string? Address { get; set; }
    public string? ImageUrl { get; set; }
} 