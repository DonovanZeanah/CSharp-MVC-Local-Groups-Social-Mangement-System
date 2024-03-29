﻿namespace WorkshopGroup.ViewModels
{
  public class EditUserDashboardViewModel
  {
    public string Id { get; set; }
    public int? Points { get; set; }
    public int? Level { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public IFormFile Image { get; set; }
    }
}
