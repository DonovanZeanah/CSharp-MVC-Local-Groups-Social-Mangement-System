using WorkshopGroup.Data;
using WorkshopGroup.Models;

namespace WorkshopGroup.Helpers
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
                new Skill { Id = 1, Name = "Skill 1", Description = "Description for Skill 1" },
                new Skill { Id = 2, Name = "Skill 2", Description = "Description for Skill 2" }
            };

                    context.Skills.AddRange(skills);
                    await context.SaveChangesAsync();
                }
            }
        }

        
    }
}
