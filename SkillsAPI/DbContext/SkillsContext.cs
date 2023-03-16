// Data/SkillContext.cs
using Microsoft.EntityFrameworkCore;
using WorkshopGroup;
using WorkshopGroup.Models;

namespace WorkshopGroup {

    public class SkillContext : DbContext
    {
        public SkillContext(DbContextOptions<SkillContext> options) : base(options)
        {
        }

        public DbSet<Skill> Skills { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
