using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using WorkshopGroup.Data;
using WorkshopGroup.Models;

namespace WorkshopGroup.Services.Helpers
{
    public class DataSeeder
    {
        public static async Task SeedDataAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();
                // Replace YourDbContext with your actual DbContext class name

                // Check if there's any data already in the Skills table
                if (!context.Skills.Any())
                {
                    // Add some sample skills
                    var skills = new List<Skill>
            {
                new Skill { Id = 1, Name = "Programming", Description = "Knows How to Code. Your crazy. Who the hell endevours to learn not math, not english, but a fusion of both...?? Insane people." },
                new Skill { Id = 2, Name = "Woodworking", Description = "Congrats. you own a mitre-saw I presume." }
            };

                    context.Skills.AddRange(skills);
                    await context.SaveChangesAsync();
                }
               
            }
        }


    }
}
