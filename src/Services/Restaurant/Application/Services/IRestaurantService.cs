using FoodOrderingSystem.Restaurant.Application.Contracts;

namespace FoodOrderingSystem.Restaurant.Application.Services;

public interface IRestaurantService
{
    // Restaurant methods
    Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
    Task<RestaurantDto?> GetRestaurantByIdAsync(Guid restaurantId);
    Task<RestaurantDto> CreateRestaurantAsync(CreateRestaurantDto createRestaurantDto);
    Task UpdateRestaurantAsync(Guid restaurantId, UpdateRestaurantDto updateRestaurantDto);
    Task DeleteRestaurantAsync(Guid restaurantId);

    // Menu methods
    Task<IEnumerable<MenuDto>> GetMenusByRestaurantIdAsync(Guid restaurantId);
    Task<MenuDto?> GetMenuByIdAsync(Guid menuId);
    Task<MenuDto> CreateMenuAsync(CreateMenuDto createMenuDto);
    Task UpdateMenuAsync(Guid menuId, UpdateMenuDto updateMenuDto);
    Task DeleteMenuAsync(Guid menuId);

    // MenuItem methods
    Task<IEnumerable<MenuItemDto>> GetMenuItemsByMenuIdAsync(Guid menuId);
    Task<MenuItemDto?> GetMenuItemByIdAsync(Guid menuItemId);
    Task<MenuItemDto> AddMenuItemAsync(CreateMenuItemDto createMenuItemDto);
    Task UpdateMenuItemAsync(Guid menuItemId, UpdateMenuItemDto updateMenuItemDto);
    Task DeleteMenuItemAsync(Guid menuItemId);

    // Category methods
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(Guid categoryId);
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto);
    Task DeleteCategoryAsync(Guid categoryId);

    // AddOn methods
    Task<IEnumerable<AddOnDto>> GetAllAddOnsAsync();
    Task<AddOnDto?> GetAddOnByIdAsync(Guid addOnId);
    Task<AddOnDto> CreateAddOnAsync(CreateAddOnDto createAddOnDto);
    Task UpdateAddOnAsync(Guid addOnId, UpdateAddOnDto updateAddOnDto);
    Task DeleteAddOnAsync(Guid addOnId);
}