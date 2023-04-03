/*using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using WorkshopGroup.Helpers;
using WorkshopGroup.Interfaces;
using WorkshopGroup.Models;
using WorkshopGroup.ViewModels;*/


using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;
using WorkshopGroup.Models;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;


/*public class HomeController : Controller
{
    IConfiguration _config;
    public HomeController(IConfiguration configuration)
    {
        _config = configuration;
    }
    [HttpGet]
    public async Task<ActionResult> Index(FlightSearchModel searchModel)
    {
        var secretValue = _config["SecretValue"];
        ViewData["TheSecretOrSomething"] = secretValue;

        if (HttpContext.Request.Method == "POST")
        {
            DateTime departureDate = DateTime.ParseExact(searchModel.DepartureDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime returnDate = DateTime.ParseExact(searchModel.ReturnDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            string formattedDepartureDate = departureDate.ToString("yyyy-MM-dd");
            string formattedReturnDate = returnDate.ToString("yyyy-MM-dd");

            string apiUrl = "https://api.tequila.kiwi.com/v2/search?fly_from=" +
                            searchModel.Origin + "&fly_to=" + searchModel.Destination +
                            "&dateFrom=" + formattedDepartureDate + "&dateTo=" + formattedReturnDate;
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.tequila.kiwi.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("apikey", "8JhlCuLlFr8NoPTC6YuEeaYUJbfOtMGw");

            // Send GET request to API
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsondata = await response.Content.ReadAsStringAsync();
                try
                {
                    // Deserialize JSON data into a list of Flight objects
                    var flightsResponse = JsonConvert.DeserializeObject<FlightsResponse>(jsondata);
                    if (flightsResponse != null) ViewBag.Flights = flightsResponse;
                    Debug.WriteLine("Api finished");
                    return PartialView("_ResultsTable");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                // Set flights as a property of the ViewBag

            }

        }
        return View(searchModel);
    }
    public IActionResult Privacy()
    {
        throw new NotImplementedException();
    }
}*/

namespace WorkshopGroup.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IClubRepository _clubRepository;

    public HomeController(ILogger<HomeController> logger, IClubRepository clubRepository)
    {
      _logger = logger;
      _clubRepository = clubRepository;
    }

    public async Task<IActionResult> Index()
    {
      var ipInfo = new IPInfo();
      var homeViewModel = new HomeViewModel();
      //TryCatchFinally
      try
      {
        string url = "https://ipinfo.io/172.56.225.177?token=aa971003411fa3";
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
          homeViewModel.Clubs = null;
        }
        return View(homeViewModel);

      }
      catch (Exception ex)
      {
        homeViewModel.Clubs = null;
      }
      return View();
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