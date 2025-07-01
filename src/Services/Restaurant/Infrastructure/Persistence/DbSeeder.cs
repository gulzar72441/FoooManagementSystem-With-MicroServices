using Bogus;
using FoodOrderingSystem.Restaurant.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Restaurant.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(RestaurantDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Restaurants.AnyAsync())
        {
            return;
        }

        // Global Categories
        var categoryFaker = new Faker<Category>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Commerce.Department());
        var allCategories = categoryFaker.Generate(10);

        var allRestaurants = new List<Domain.Entities.Restaurant>();
        var allMenus = new List<Menu>();
        var allMenuItems = new List<MenuItem>();
        var allAddOns = new List<AddOn>();

        var restaurantFaker = new Faker<Domain.Entities.Restaurant>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Name, f => f.Company.CompanyName())
            .RuleFor(r => r.Address, f => f.Address.FullAddress())
            .RuleFor(r => r.CuisineType, f => f.PickRandom("Italian", "Mexican", "Chinese", "Indian", "American"))
            .RuleFor(r => r.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(r => r.OwnerId, f => Guid.NewGuid());

        var restaurants = restaurantFaker.Generate(10);
        allRestaurants.AddRange(restaurants);

        foreach (var restaurant in restaurants)
        {
            var menuFaker = new Faker<Menu>()
                .RuleFor(m => m.Id, f => Guid.NewGuid())
                .RuleFor(m => m.Name, f => f.PickRandom("Breakfast", "Lunch", "Dinner", "Specials"))
                .RuleFor(m => m.RestaurantId, restaurant.Id);
            
            var menus = menuFaker.Generate(2);
            allMenus.AddRange(menus);

            foreach (var menu in menus)
            {
                var menuItemFaker = new Faker<MenuItem>()
                    .RuleFor(mi => mi.Id, f => Guid.NewGuid())
                    .RuleFor(mi => mi.Name, f => f.Commerce.ProductName())
                    .RuleFor(mi => mi.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(mi => mi.Price, f => f.Random.Decimal(5, 50))
                    .RuleFor(mi => mi.ImageUrl, f => f.Lorem.Word())
                    .RuleFor(mi => mi.MenuId, menu.Id)
                    .RuleFor(mi => mi.CategoryId, f => f.PickRandom(allCategories).Id);
                
                var menuItems = menuItemFaker.Generate(10);
                allMenuItems.AddRange(menuItems);

                foreach (var menuItem in menuItems)
                {
                    var f = new Faker();
                    if (f.Random.Bool())
                    {
                        var addOnFaker = new Faker<AddOn>()
                            .RuleFor(a => a.Id, f => Guid.NewGuid())
                            .RuleFor(a => a.Name, f => f.PickRandom("Extra Cheese", "Spicy Sauce", "Bacon"))
                            .RuleFor(a => a.Price, f => f.Random.Decimal(1, 5));
                        
                        var addOns = addOnFaker.Generate(f.Random.Int(1, 3));
                        foreach (var addon in addOns)
                        {
                            menuItem.AddOns.Add(addon);
                            allAddOns.Add(addon);
                        }
                    }
                }
            }
        }

        await context.Categories.AddRangeAsync(allCategories);
        await context.Restaurants.AddRangeAsync(allRestaurants);
        await context.Menus.AddRangeAsync(allMenus);
        await context.MenuItems.AddRangeAsync(allMenuItems);
        await context.AddOns.AddRangeAsync(allAddOns);

        await context.SaveChangesAsync();
    }
}