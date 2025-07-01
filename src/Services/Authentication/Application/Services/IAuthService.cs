using FoodOrderingSystem.Authentication.Application.Contracts;

namespace FoodOrderingSystem.Authentication.Application.Services;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshTokenAsync(TokenRequest request);
    Task RevokeTokenAsync(string refreshToken);
    Task<UserDto> GetUserByIdAsync(Guid userId);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserDto dto);
    Task DeleteUserAsync(Guid userId);

    // Role Management
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    Task<RoleDto> CreateRoleAsync(CreateRoleDto dto);
    Task DeleteRoleAsync(Guid roleId);
} 