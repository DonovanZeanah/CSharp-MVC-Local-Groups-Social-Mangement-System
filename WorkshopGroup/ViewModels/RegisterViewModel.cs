using System.ComponentModel.DataAnnotations;

namespace WorkshopGroup.ViewModels
{
  public class RegisterViewModel
  {
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email required.. so I can spam your inbox until it's un-usable.")]
    public string EmailAddress { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Display(Name = "Confirm Password")] //Not for validation, just for the view.
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }
  }
}
