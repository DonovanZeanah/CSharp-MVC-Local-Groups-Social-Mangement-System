using Microsoft.AspNetCore.Identity;
using WorkshopGroup.Data;
using WorkshopGroup.Models;

namespace WorkshopGroup.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<IdentityRole>>();


            return services;
        }

        public static async Task SeedRoles(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed User role
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                var role = new IdentityRole(UserRoles.User);
                await roleManager.CreateAsync(role);
            }

            // Seed Admin role
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                var role = new IdentityRole(UserRoles.Admin);
                await roleManager.CreateAsync(role);
            }

            // Seed Moderator role
            if (!await roleManager.RoleExistsAsync(UserRoles.Moderator))
            {
                var role = new IdentityRole(UserRoles.Moderator);
                await roleManager.CreateAsync(role);
            }

            // Seed Leader role
            if (!await roleManager.RoleExistsAsync(UserRoles.Leader))
            {
                var role = new IdentityRole(UserRoles.Leader);
                await roleManager.CreateAsync(role);
            }

            // Seed Guru role
            if (!await roleManager.RoleExistsAsync(UserRoles.Guru))
            {
                var role = new IdentityRole(UserRoles.Guru);
                await roleManager.CreateAsync(role);
            }

            ///
            // Seed admin and moderator users
            await SeedUsers(scope.ServiceProvider);

        }

        public static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

            // Seed 1 admin user
            var adminUser = new AppUser
            {
                Id = "1",
                UserId = "1",
                UserName = "admin@example.com",
                Email = "admin@example.com"
            };
            var adminUserResult = await userManager.CreateAsync(adminUser, "AdminPassword123!");
            if (adminUserResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
            }

            // Seed 3 moderator users
            for (int i = 2; i <= 5; i++)
            {
                var moderatorUser = new AppUser
                {
                    Id = i.ToString(),
                    UserId = i.ToString(),
                    UserName = $"moderator{i}@example.com",
                    Email = $"moderator{i}@example.com"
                };
                var moderatorUserResult = await userManager.CreateAsync(moderatorUser, $"Moderator{i}Password123!");
                if (moderatorUserResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(moderatorUser, UserRoles.Moderator);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }

    }

    /////
}
