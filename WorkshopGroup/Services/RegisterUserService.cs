using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkshopGroup.Controllers;
using WorkshopGroup.Data;
using WorkshopGroup.Models;
using WorkshopGroup.Services.interfaces;
using WorkshopGroup.ViewModels;

namespace WorkshopGroup.Services
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterUserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(string emailAddress, string password)
        {
            var newUser = new AppUser
            {
                Email = emailAddress,
                UserName = emailAddress,
                Points = 100,
                Level = 1,
                ProfileImageUrl = "/img/default.jpg"
            };

            var result = await _userManager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser.ToString(), UserRoles.User);
            }

            return result;
        }
    }
  
 
   /* private readonly IRegisterUserService _registerUserService;

    public HomeController(IUserService userService)
    {
        _userService = userService;
    }

        [HttpPost]
    public async Task<IActionResult> Register(HomeUserCreateViewModel userCreateViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userCreateViewModel);
        }

        var result = await _userService.RegisterUserAsync(userCreateViewModel.EmailAddress, userCreateViewModel.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(await _userManager.FindByEmailAsync(userCreateViewModel.EmailAddress), false);
            return RedirectToAction("Index", "Home");
        }

        TempData["Error"] = result.Errors.FirstOrDefault()?.Description;
        return View(userCreateViewModel);
    }*/
    //This way, you can reuse the registration logic and have both controllers use the same functionality without duplicating code.






}
