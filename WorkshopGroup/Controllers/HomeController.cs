using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using WorkshopGroup.Models;
using WorkshopGroup.Repository.Interfaces;
using WorkshopGroup.Services;
using WorkshopGroup.Services.Helpers;
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


    public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ILocationService locationService)
    {
      _logger = logger;
      _clubRepository = clubRepository;
      _userManager = userManager;
      _signInManager = signInManager;
      _locationService = locationService;
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
        if(homeViewModel.City != null) 
        {
          homeViewModel.Clubs = await _clubRepository.GetClubByCity(homeViewModel.City);

        }
        else
        {
            homeViewModel.City = "default city";
            homeViewModel.State = "default state";
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

    public IActionResult Register()
    {
        var response = new HomeUserCreateViewModel();
        return View(response);
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