using ECommerce.API.Auth;
using ECommerce.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Data.Seed;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration configuration, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var serviceProvider = scope.ServiceProvider;

        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DataSeeder");

        await dbContext.Database.MigrateAsync(cancellationToken);

        foreach (var role in RoleConstants.AllRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        var adminEmail = "admin@example.com";
        var adminPassword = configuration["Seed:AdminPassword"];
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser is null)
        {
            if (string.IsNullOrWhiteSpace(adminPassword))
            {
                logger.LogWarning("Skipping admin creation because Seed:AdminPassword is missing.");
            }
            else
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Admin",
                    EmailConfirmed = true
                };
                var createResult = await userManager.CreateAsync(adminUser, adminPassword);
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RoleConstants.Admin);
                }
                else
                {
                    logger.LogWarning("Failed to create seed admin user: {Errors}", string.Join("; ", createResult.Errors.Select(x => x.Description)));
                }
            }
        }

        if (await dbContext.Categories.AnyAsync(cancellationToken))
        {
            return;
        }

        var categories = new List<Category>
        {
            new() { Name = "Electronics", Slug = "electronics", Description = "Phones, laptops, and gadgets" },
            new() { Name = "Fashion", Slug = "fashion", Description = "Clothing and accessories" },
            new() { Name = "Home", Slug = "home", Description = "Home and kitchen essentials" },
            new() { Name = "Beauty", Slug = "beauty", Description = "Beauty and personal care" },
            new() { Name = "Sports", Slug = "sports", Description = "Fitness and outdoor equipment" }
        };

        dbContext.Categories.AddRange(categories);
        await dbContext.SaveChangesAsync(cancellationToken);

        var products = new List<Product>();
        var counter = 1;
        foreach (var category in categories)
        {
            for (var i = 1; i <= 4; i++)
            {
                products.Add(new Product
                {
                    Name = $"{category.Name} Product {i}",
                    Slug = $"{category.Slug}-product-{i}",
                    Description = $"Sample product {counter} in the {category.Name} category.",
                    Price = 10 + (counter * 3),
                    DiscountPrice = counter % 2 == 0 ? 8 + (counter * 2) : null,
                    SKU = $"SKU-{counter:0000}",
                    StockQuantity = 20 + counter,
                    CategoryId = category.CategoryId,
                    ImageUrl = "https://placehold.co/600x400"
                });

                counter++;
            }
        }

        dbContext.Products.AddRange(products);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
