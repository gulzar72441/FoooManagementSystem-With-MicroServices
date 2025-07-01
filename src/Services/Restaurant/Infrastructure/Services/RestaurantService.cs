using FoodOrderingSystem.Restaurant.Application.Contracts;
using FoodOrderingSystem.Restaurant.Application.Services;
using FoodOrderingSystem.Restaurant.Domain.Entities;
using FoodOrderingSystem.Restaurant.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Restaurant.Infrastructure.Services;

public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _context;

    public RestaurantService(RestaurantDbContext context)
    {
        _context = context;
    }

    // Restaurant methods
    public async Task<RestaurantDto> CreateRestaurantAsync(CreateRestaurantDto createDto)
    {
        var restaurant = new Domain.Entities.Restaurant
        {
            Name = createDto.Name,
            Address = createDto.Address,
            CuisineType = createDto.CuisineType,
            PhoneNumber = createDto.ContactNumber,
            OwnerId = createDto.OwnerId
        };

        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();

        return new RestaurantDto
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Address = restaurant.Address,
            CuisineType = restaurant.CuisineType,
            ContactNumber = restaurant.PhoneNumber,
            OwnerId = restaurant.OwnerId
        };
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        return await _context.Restaurants
            .Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                CuisineType = r.CuisineType,
                ContactNumber = r.PhoneNumber,
                OwnerId = r.OwnerId
            })
            .ToListAsync();
    }

    public async Task<RestaurantDto?> GetRestaurantByIdAsync(Guid restaurantId)
    {
        var restaurant = await _context.Restaurants
            .Where(r => r.Id == restaurantId)
            .Select(r => new RestaurantDto
            {
                Id = r.Id,
                Name = r.Name,
                Address = r.Address,
                CuisineType = r.CuisineType,
                ContactNumber = r.PhoneNumber,
                OwnerId = r.OwnerId
            })
            .FirstOrDefaultAsync();

        return restaurant;
    }

    public async Task UpdateRestaurantAsync(Guid restaurantId, UpdateRestaurantDto updateDto)
    {
        var restaurant = await _context.Restaurants.FindAsync(restaurantId);
        if (restaurant == null) throw new Exception("Restaurant not found");

        restaurant.Name = updateDto.Name;
        restaurant.Address = updateDto.Address;
        restaurant.CuisineType = updateDto.CuisineType;
        restaurant.PhoneNumber = updateDto.PhoneNumber;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteRestaurantAsync(Guid restaurantId)
    {
        var restaurant = await _context.Restaurants.FindAsync(restaurantId);
        if (restaurant == null) throw new Exception("Restaurant not found");

        _context.Restaurants.Remove(restaurant);
        await _context.SaveChangesAsync();
    }

    // Menu methods
    public async Task<IEnumerable<MenuDto>> GetMenusByRestaurantIdAsync(Guid restaurantId)
    {
        return await _context.Menus
            .Where(m => m.RestaurantId == restaurantId)
            .Select(m => new MenuDto
            {
                Id = m.Id,
                Name = m.Name,
                RestaurantId = m.RestaurantId
            })
            .ToListAsync();
    }

    public async Task<MenuDto?> GetMenuByIdAsync(Guid menuId)
    {
        return await _context.Menus
            .Where(m => m.Id == menuId)
            .Select(m => new MenuDto
            {
                Id = m.Id,
                Name = m.Name,
                RestaurantId = m.RestaurantId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MenuDto> CreateMenuAsync(CreateMenuDto createMenuDto)
    {
        var menu = new Menu
        {
            Name = createMenuDto.Name,
            RestaurantId = createMenuDto.RestaurantId
        };

        await _context.Menus.AddAsync(menu);
        await _context.SaveChangesAsync();

        return new MenuDto { Id = menu.Id, Name = menu.Name, RestaurantId = menu.RestaurantId };
    }

    public async Task UpdateMenuAsync(Guid menuId, UpdateMenuDto updateMenuDto)
    {
        var menu = await _context.Menus.FindAsync(menuId);
        if (menu == null) throw new Exception("Menu not found");

        menu.Name = updateMenuDto.Name;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMenuAsync(Guid menuId)
    {
        var menu = await _context.Menus.FindAsync(menuId);
        if (menu == null) throw new Exception("Menu not found");

        _context.Menus.Remove(menu);
        await _context.SaveChangesAsync();
    }

    // MenuItem methods
    public async Task<IEnumerable<MenuItemDto>> GetMenuItemsByMenuIdAsync(Guid menuId)
    {
        return await _context.MenuItems
            .Where(mi => mi.MenuId == menuId)
            .Select(mi => new MenuItemDto
            {
                Id = mi.Id,
                Name = mi.Name,
                Description = mi.Description,
                Price = mi.Price,
                ImageUrl = mi.ImageUrl
            })
            .ToListAsync();
    }

    public async Task<MenuItemDto?> GetMenuItemByIdAsync(Guid menuItemId)
    {
        var item = await _context.MenuItems
            .Where(i => i.Id == menuItemId)
            .Select(i => new MenuItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                ImageUrl = i.ImageUrl
            })
            .FirstOrDefaultAsync();
        
        return item;
    }
    
    public async Task<MenuItemDto> AddMenuItemAsync(CreateMenuItemDto createDto)
    {
        var menuItem = new MenuItem
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Price = createDto.Price,
            ImageUrl = createDto.ImageUrl,
            MenuId = createDto.MenuId,
            CategoryId = createDto.CategoryId
        };

        await _context.MenuItems.AddAsync(menuItem);
        await _context.SaveChangesAsync();

        return new MenuItemDto { Id = menuItem.Id, Name = menuItem.Name, Description = menuItem.Description, Price = menuItem.Price, ImageUrl = menuItem.ImageUrl };
    }

    public async Task UpdateMenuItemAsync(Guid menuItemId, UpdateMenuItemDto updateDto)
    {
        var menuItem = await _context.MenuItems.FindAsync(menuItemId);
        if (menuItem == null) throw new Exception("Menu item not found");

        if (updateDto.Name != null) menuItem.Name = updateDto.Name;
        if (updateDto.Description != null) menuItem.Description = updateDto.Description;
        if (updateDto.Price.HasValue) menuItem.Price = updateDto.Price.Value;
        if (updateDto.ImageUrl != null) menuItem.ImageUrl = updateDto.ImageUrl;
        if (updateDto.CategoryId.HasValue) menuItem.CategoryId = updateDto.CategoryId.Value;
        
        await _context.SaveChangesAsync();
    }

    public async Task DeleteMenuItemAsync(Guid menuItemId)
    {
        var menuItem = await _context.MenuItems.FindAsync(menuItemId);
        if (menuItem == null) throw new Exception("Menu item not found");

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();
    }

    // Category methods
    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        return await _context.Categories
            .Select(c => new CategoryDto { Id = c.Id, Name = c.Name })
            .ToListAsync();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(Guid categoryId)
    {
        return await _context.Categories
            .Where(c => c.Id == categoryId)
            .Select(c => new CategoryDto { Id = c.Id, Name = c.Name })
            .FirstOrDefaultAsync();
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var category = new Category { Name = createCategoryDto.Name };
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return new CategoryDto { Id = category.Id, Name = category.Name };
    }

    public async Task UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category == null) throw new Exception("Category not found");

        category.Name = updateCategoryDto.Name;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(Guid categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId);
        if (category == null) throw new Exception("Category not found");

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    // AddOn methods
    public async Task<IEnumerable<AddOnDto>> GetAllAddOnsAsync()
    {
        return await _context.AddOns
            .Select(a => new AddOnDto { Id = a.Id, Name = a.Name, Price = a.Price })
            .ToListAsync();
    }

    public async Task<AddOnDto?> GetAddOnByIdAsync(Guid addOnId)
    {
        return await _context.AddOns
            .Where(a => a.Id == addOnId)
            .Select(a => new AddOnDto { Id = a.Id, Name = a.Name, Price = a.Price })
            .FirstOrDefaultAsync();
    }

    public async Task<AddOnDto> CreateAddOnAsync(CreateAddOnDto createAddOnDto)
    {
        var addOn = new AddOn { Name = createAddOnDto.Name, Price = createAddOnDto.Price };
        await _context.AddOns.AddAsync(addOn);
        await _context.SaveChangesAsync();
        return new AddOnDto { Id = addOn.Id, Name = addOn.Name, Price = addOn.Price };
    }

    public async Task UpdateAddOnAsync(Guid addOnId, UpdateAddOnDto updateAddOnDto)
    {
        var addOn = await _context.AddOns.FindAsync(addOnId);
        if (addOn == null) throw new Exception("AddOn not found");

        addOn.Name = updateAddOnDto.Name;
        if (updateAddOnDto.Price.HasValue) addOn.Price = updateAddOnDto.Price.Value;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAddOnAsync(Guid addOnId)
    {
        var addOn = await _context.AddOns.FindAsync(addOnId);
        if (addOn == null) throw new Exception("AddOn not found");

        _context.AddOns.Remove(addOn);
        await _context.SaveChangesAsync();
    }
} 