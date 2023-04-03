using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkshopGroup.Models;

namespace WorkshopGroup.Data
{
    //   public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, etc>

    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //=========================================================================================
        public DbSet<Project> Projects { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public DbSet<Tool> Tools { get; set; }
        //=========================================================================================
        public DbSet<Category> Categories { get; set; }

        public DbSet<Supply> Supplies { get; set; }
        // public DbSet<Owner> Owners { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<SupplyCategory> SupplyCategories { get; set; }
        public DbSet<SupplyOwner> SupplyOwners { get; set; }
        //;[]==============================[]=================================[]        
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            /*modelbuilder.Entity<Supply>()
              .HasMany(s => s.Reviews)
              .WithOne(r => r.Supply)
              .HasForeignKey(r => r.SupplyId);

            modelbuilder.Entity<Supply>()
              .HasMany(s => s.Reviews)
              .WithOne(r => r.Supply)
              .HasForeignKey(r => r.SupplyId);*/

            modelbuilder.Entity<Review>()
              .HasOne(r => r.Reviewer)
              .WithMany(rw => rw.Reviews)
              .HasForeignKey(r => r.ReviewerId);

            modelbuilder.Entity<SupplyCategory>()
              .HasKey(sc => new { sc.SupplyId, sc.CategoryId });

            modelbuilder.Entity<SupplyCategory>()
               .HasOne(s => s.Supply)
               .WithMany(s => s.SupplyCategories)
               .HasForeignKey(c => c.SupplyId);

            modelbuilder.Entity<SupplyCategory>()
               .HasOne(s => s.Category)
               .WithMany(s => s.SupplyCategories)
               .HasForeignKey(c => c.CategoryId);

            modelbuilder.Entity<SupplyOwner>()
              .HasKey(so => new { so.SupplyId, so.OwnerId });

            modelbuilder.Entity<SupplyOwner>()
               .HasOne(s => s.Supply)
               .WithMany(s => s.SupplyOwners)
               .HasForeignKey(c => c.SupplyId);

            modelbuilder.Entity<SupplyOwner>()
               .HasOne(s => s.Owner)
               .WithMany(s => s.SupplyOwners)
               .HasForeignKey(c => c.OwnerId);

            modelbuilder.Entity<IdentityUserLogin<string>>()
              .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });
            modelbuilder.Entity<IdentityUserRole<string>>()
              .HasKey(l => new { l.UserId, l.RoleId });
            modelbuilder.Entity<IdentityUserToken<string>>()
              .HasKey(l => new { l.UserId, l.LoginProvider, l.Name });


        }

    }
}
