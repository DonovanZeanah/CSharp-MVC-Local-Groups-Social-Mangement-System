using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WorkshopGroup.Models
{

  public class AppUser : IdentityUser, IUser<string>
  {
    //public string Id { get; set; }
    // key is string (guid) after IdentityUser implimented
    [Key]
    public string Id { get; set; }
    public string? UserId { get; set; }
    //public string? UserId { get; set; }
    public string? UserName { get; set; }
    public double? Points { get; set; }
    public int? Level { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public Address? Address { get; set; }
    //[ForeignKey(nameof(Address))]
    [ForeignKey("Address")]
    public int? AddressId { get; set; }
    public ICollection<Club>? Clubs { get; set; }
    public ICollection<Project>? Projects { get; set; }
    public ICollection<Skill>? Skills { get; set; } //= new List<Skill>();

    public AppUser() { } // parameterless constructor

    public AppUser(string userName) : base(userName) { } // constructor with userName parameter

    // additional properties and methods
    }
}
