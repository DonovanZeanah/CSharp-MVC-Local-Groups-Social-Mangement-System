namespace WorkshopGroup.ViewModels
{
    public class EditProfileViewModel
    {
        public int? Points { get; set; }
        public int? Level { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public IFormFile? Image { get; set; }
        public int? Level { get; internal set; }
    }
}