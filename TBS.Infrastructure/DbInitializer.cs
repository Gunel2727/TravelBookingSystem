using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TBS.Domain.Entitites;
using TBS.Infrastructure.Data;

namespace TBS.Infrastructure
{
    public static class DbInitializer
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

          
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

           
            var adminEmail = "admin@tbs.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin_user",
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    PhoneNumber = "+994501234567",
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(adminUser, "Password123!");
                if (createResult.Succeeded)
                {
                    
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
            else
            {
                
                if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}