using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopGroup.Models;

namespace WorkshopGroup.Data
{
  //   public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, etc>

  public class ApplicationDbContext : IdentityDbContext<AppUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
    }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Club> Clubs { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<City> Cities { get; set; }


  }
}
