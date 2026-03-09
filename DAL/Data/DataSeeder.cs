using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestaurantManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.DAL.Data
{
    public static class DataSeeder
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var provider = scope.ServiceProvider;
            var logger = provider.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");

            var context = provider.GetRequiredService<RestaurantDBContext>();
            // Apply pending migrations (if any)
            await context.Database.MigrateAsync();

            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = provider.GetRequiredService<UserManager<User>>();

            string[] roles = new[] { "Admin", "Cook", "Waiter", "Cashier" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var createRoleResult = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!createRoleResult.Succeeded)
                    {
                        logger.LogWarning("Failed to create role {Role}: {Errors}", role, string.Join(", ", createRoleResult.Errors));
                    }
                }
            }

            // Seed an admin user
            var adminEmail = "admin@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Administrator",
                    EmailConfirmed = true,
                    Status = "Active",
                    Role = "Admin"
                };

                var createUserResult = await userManager.CreateAsync(adminUser, "Admin@123");
                if (createUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    logger.LogWarning("Failed to create admin user: {Errors}", string.Join(", ", createUserResult.Errors));
                }
            }
        }
    }
}
