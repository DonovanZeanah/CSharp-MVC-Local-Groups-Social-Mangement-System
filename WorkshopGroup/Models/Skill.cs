using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkshopGroup.Models
{
  public class Skill
  {
    public int Id { get; set; }
    public string Name { get; set;}
    //public List<Tool> Tools { get; set; }
    [ForeignKey("AppUser")]
    public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
  }
}
