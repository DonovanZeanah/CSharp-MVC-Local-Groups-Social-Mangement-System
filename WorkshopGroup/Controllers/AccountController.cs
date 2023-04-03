using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkshopGroup.Data;
using WorkshopGroup.Models;
using WorkshopGroup.ViewModels;

namespace WorkshopGroup.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ApplicationDbContext _context;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _context = context;
    }


    //Defaults to a get
    public IActionResult Login()
    {
      var response = new LoginViewModel();
      return View(response);
    }
    // results in a post, comes in tandem with the above git, both named "login"
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
      if (!ModelState.IsValid) return View(loginViewModel);

      var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
      if (user != null)
      {
        var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
        if (passwordCheck)
        {
          var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
          if (result.Succeeded)
          {
            return RedirectToAction("Index", "Project");
          }
        }

        //logic if pass fails, todo: impliment a loginViewModel.Password isCorrect vice tempdata

        //Pass Incorrect.
        TempData["Error"] = "No street creds, sorry";
        return View(loginViewModel);
      }
      //User Incorrect.
      TempData["Error"] = "No Street creds, sorry";
      return View(loginViewModel);


    }

    //Defaults to a get
    public IActionResult Register()
    {
      var response = new RegisterViewModel();
      return View(response);
    }
    // results in a post, comes in tandem with the above git, both named "login"
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
      if (!ModelState.IsValid) return View(registerViewModel);

      var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
      if (user != null)
      {
        TempData["Error"] = "Email already in use";
        return View(registerViewModel);
      }
       var newUser = new AppUser()
      {
        Email = registerViewModel.EmailAddress,
        UserName = registerViewModel.EmailAddress
      };
      var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);
      if (newUserResponse.Succeeded)
         await _userManager.AddToRoleAsync(newUser, UserRoles.User);
      
      return RedirectToAction("Login", "Account");
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Project");
    }
  }
}
