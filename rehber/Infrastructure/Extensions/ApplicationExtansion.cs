using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace rehber.Infrastructure.Extensions
{
    public static class ApplicationExtansion
    {
        public static void ConfigureAndCheckMigrations(this IApplicationBuilder app)
        {
            DataContext context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<DataContext>();
            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

        }

        public static void ConfigureLocalization(this WebApplication app)
        {
            app.UseRequestLocalization(options =>
            {
                options.AddSupportedCultures("tr-TR")
                .AddSupportedCultures("tr-TR")
                .SetDefaultCulture("tr-TR");

                options.AddSupportedCultures("en-US")
                .AddSupportedCultures("en-US")
                .SetDefaultCulture("en-US");

                options.AddSupportedCultures("en-GB")
                .AddSupportedCultures("en-GB")
                .SetDefaultCulture("en-GB");
            });
        }

        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app)
        {
            const string adminUser = "Admin";
            const string adminPassword = "Admin_123456";

            // UserManager
            UserManager<Users> userManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<UserManager<Users>>();

            // RoleManager
            RoleManager<IdentityRole> roleManager = app
                .ApplicationServices
                .CreateAsyncScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            Users user = await userManager.FindByNameAsync(adminUser);
            if (user is null)
            {
                user = new Users()
                {
                    Email = "gmail@gmail.com",
                    PhoneNumber = "5061112233",
                    UserName = adminUser,
                    SessionTimeoutInHours =2,
                    AddedByUserName = "Supreme"
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (!result.Succeeded)
                {
                    string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"Admin user could not be created. Errors: {errors}");
                }

                var roleResult = await userManager.AddToRolesAsync(user,
                    roleManager
                        .Roles
                        .Select(r => r.Name)
                        .ToList()
                );

                if (!roleResult.Succeeded)
                    throw new Exception("System have problems with role defination for admin.");
            }
        }
    }
}
