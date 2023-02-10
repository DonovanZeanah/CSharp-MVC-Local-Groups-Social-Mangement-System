using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopGroup.Models
{
  public class AppUser : IdentityUser
  {
/*    [Key]
    public string Id { get; set; }*/

    public double? points { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public Address? Address { get; set; }
    //[ForeignKey(nameof(Address))]
    [ForeignKey("Address")]
    public int? AddressId { get; set; }
    public ICollection<Club>? Clubs { get; set; }
    public ICollection<Project>? Projects { get; set; }

  }
}
