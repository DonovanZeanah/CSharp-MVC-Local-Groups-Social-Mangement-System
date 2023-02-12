using WorkshopGroup.Data.Enum;
using WorkshopGroup.Models;

namespace WorkshopGroup.ViewModels
{
  public class CreateProjectViewModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Address Address { get; set; }
    public IFormFile Image { get; set; }
    public ProjectCategory ProjectCategory { get; set; }
    public string AppUserId { get; set; }
  }
}
