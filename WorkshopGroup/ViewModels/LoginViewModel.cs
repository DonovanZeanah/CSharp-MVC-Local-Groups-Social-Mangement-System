using System.ComponentModel.DataAnnotations;

namespace WorkshopGroup.ViewModels
{
  public class LoginViewModel
  {
    //validation put in ViewModels, not 'Domain' Models


    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email required.. so I can spam your inbox until it's un-usable.")]
    public string EmailAddress { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

  }
}
