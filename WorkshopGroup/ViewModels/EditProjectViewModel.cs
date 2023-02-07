using WorkshopGroup.Data.Enum;
using WorkshopGroup.Models;

namespace WorkshopGroup.ViewModels
{
  public class EditProjectViewModel
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public IFormFile Image { get; set; }
    public string? URL { get; set; }
    public int? AddressId { get; set; }
    public Address? Address { get; set; }
    public ProjectCategory ProjectCategory { get; set; }
  }
}
