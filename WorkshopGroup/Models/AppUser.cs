using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopGroup.Models
{
  public class AppUser //: IdentityUser
  {
    [Key]
    public string Id { get; set; }

    public double? points { get; set; }
    public Address? Address { get; set; }
    public ICollection<Club> Clubs { get; set; }
    public ICollection<Project> Projects { get; set; }

  }
}
