using WorkshopGroup.Models;

namespace WorkshopGroup.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Club>? Clubs { get; set; } = new List<Club>();
        public string? City { get; set; }
        public string? State { get; set; }
        public HomeUserCreateViewModel? Register { get; set; } = new HomeUserCreateViewModel();
    }
}