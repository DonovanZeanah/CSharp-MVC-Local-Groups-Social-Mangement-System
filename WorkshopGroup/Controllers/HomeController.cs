using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using WorkshopGroup.Data;
using WorkshopGroup.Models;
using WorkshopGroup.Repository.Interfaces;
using WorkshopGroup.Services;
using WorkshopGroup.Services.Helpers;
using WorkshopGroup.Services.interfaces;
using WorkshopGroup.ViewModels;

namespace WorkshopGroup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository _clubRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILocationService _locationService;
        //private readonly AccountController _accountController;
        //private readonly RegisterUserService _registerUserService;


        public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILocationService locationService)
        {
            _logger = logger;
            _clubRepository = clubRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _locationService = locationService;
            //_accountController = accountController;
            //_registerUserService = registerUserService;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var homeViewModel = new HomeViewModel();
            //TryCatchFinally
            try
            {
                string url = "https://ipinfo.io?token=aa971003411fa3";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeViewModel.City = ipInfo.City;
                homeViewModel.State = ipInfo.Region;
                if (homeViewModel.City != null)
                {
                    homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);

                }
                else
                {
                    homeViewModel.City = "Tuscaloosa";
                    homeViewModel.State = "Alabama";
                    homeViewModel.Clubs = new List<Club>();
                    //homeViewModel.Clubs = null;
                }
                return View(homeViewModel);

            }
            catch (Exception ex)
            {
                homeViewModel.City = "Tuscaloosa";
                homeViewModel.State = "Alabama";
                homeViewModel.Clubs = new List<Club>();
                //homeViewModel.Clubs = null;
            }
            return View(homeViewModel);
        }

        /*[HttpPost]
        public async Task<IActionResult> Register(HomeUserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid) return View(userCreateViewModel);

            var result =
                await _registerUserService.RegisterUserAsync(userCreateViewModel.EmailAddress,
                    userCreateViewModel.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(await _userManager.FindByEmailAsync(userCreateViewModel.EmailAddress),
                    false);
                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = result.Errors.FirstOrDefault();
            return View(userCreateViewModel);
        }*/
        [HttpPost]
        public async Task<IActionResult> Register(HomeUserCreateViewModel userCreateViewModel)
        {
            if (!ModelState.IsValid) return View(userCreateViewModel);

            var user = await _userManager.FindByEmailAsync(userCreateViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Email already in use";
                return View(userCreateViewModel);
            }

            var newUser = new AppUser()
            {
                Email = userCreateViewModel.EmailAddress,
                UserName = userCreateViewModel.EmailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, userCreateViewModel.Password);
            if (newUserResponse.Succeeded)
            {
                Console.WriteLine("should redirect.");
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return RedirectToAction("Login", "Account");
            }
            Console.WriteLine("skipped over, not redirecting.");
            foreach (var error in newUserResponse.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userCreateViewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

/*var registerViewModel = new RegisterViewModel()
{
    EmailAddress = registerViewModel.EmailAddress,
    Password = registerViewModel.Password,
    ConfirmPassword = registerViewModel.ConfirmPassword
};*/

/*var result = await _accountController.Register(registerViewModel);
if (result is RedirectToActionResult)
{
await _signInManager.SignInAsync(await _userManager.FindByEmailAsync(userCreateViewModel.EmailAddress), false);
return RedirectToAction("Index", "Home");
}
else if (result is ViewResult)
{
return View(userCreateViewModel);
}
else
{
// handle failure cases here
// ...
return View(userCreateViewModel);
}
}*/


/*[HttpPost]
public async Task<IActionResult> Register(HomeUserCreateViewModel userCreateViewModel)
{
    if (!ModelState.IsValid) return View(userCreateViewModel);

    var user = await _userManager.FindByEmailAsync(userCreateViewModel.EmailAddress);
    if (user != null)
    {
        TempData["Error"] = "Email already in use";
        return View(userCreateViewModel);
    }

    var newUser = new AppUser()
    {
        Email = userCreateViewModel.EmailAddress,
        UserName = userCreateViewModel.EmailAddress
    };

    var newUserResponse = await _userManager.CreateAsync(newUser, userCreateViewModel.Password);
    if (newUserResponse.Succeeded)
    {
        await _userManager.AddToRoleAsync(newUser, UserRoles.User);
        await _signInManager.SignInAsync(newUser, false);
        return RedirectToAction("Index", "Home");
    }

    // handle failure cases here
    // ...

    return View(userCreateViewModel);
}*/






/* public IActionResult Register()
 {
     var response = new HomeUserCreateViewModel();
     return View(response);
 }*/
