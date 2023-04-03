using Microsoft.AspNetCore.Identity;
using WorkshopGroup.Data;

namespace WorkshopGroup.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static async Task SeedRoles(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            Console.WriteLine();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                var role = new IdentityRole(UserRoles.User);
                await roleManager.CreateAsync(role);
            }
        }

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


/////
    }
}