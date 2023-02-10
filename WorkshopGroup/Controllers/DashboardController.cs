using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkshopGroup.Interfaces;
using WorkshopGroup.Models;
using WorkshopGroup.ViewModels;
using WorkshopGroup.Interfaces;

namespace WorkshopGroup.Controllers
{
  [Authorize]
  public class DashboardController : Controller
  {
    private readonly IDashboardRepository _dashboardRespository;
    private readonly IPhotoService _photoService;

    public DashboardController(IDashboardRepository dashboardRespository, IPhotoService photoService)
    {
      _dashboardRespository = dashboardRespository;
      _photoService = photoService;
    }

    public async Task<IActionResult> Index()
    {
      var userProjects = await _dashboardRespository.GetAllUserProjects();
      var userClubs = await _dashboardRespository.GetAllUserClubs();
      var dashboardViewModel = new DashboardViewModel()
      {
        Projects = userProjects,
        Clubs = userClubs
      };
      return View(dashboardViewModel);
    }
  }
}