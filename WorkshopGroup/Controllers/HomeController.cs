﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RunGroopWebApp.Helpers;
using RunGroopWebApp.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using WorkshopGroup.Helpers;
using WorkshopGroup.Interfaces;
using WorkshopGroup.Models;
using WorkshopGroup.ViewModels;

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