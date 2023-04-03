using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkshopGroup.Models;
using WorkshopGroup.ViewModels;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.PointsToAnalysis;
using WorkshopGroup.Repository;
using WorkshopGroup.Services;
using WorkshopGroup.Repository.Interfaces;

namespace WorkshopGroup.Controllers
{
    [Authorize]
  public class DashboardController : Controller
  {
    private readonly IDashboardRepository _dashboardRepository;
    private readonly IPhotoService _photoService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
    {
      _dashboardRepository = dashboardRepository;
      _httpContextAccessor = httpContextAccessor;
      _photoService = photoService;
    }
    private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
    {
      user.Id = editVM.Id;
      user.Points = editVM.Points;
      user.Mileage = editVM.Mileage;
      user.ProfileImageUrl = photoResult.Url.ToString();
      user.City = editVM.City;
      user.State = editVM.State;
    }


    public async Task<IActionResult> Index()
    {
      var userProjects = await _dashboardRepository.GetAllUserProjects();
      var userClubs = await _dashboardRepository.GetAllUserClubs();
      var dashboardViewModel = new DashboardViewModel()
      {
        Projects = userProjects,
        Clubs = userClubs
      };
      return View(dashboardViewModel);
    }

    public async Task<IActionResult> EditUserProfile()
    {
      var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
      var user = await _dashboardRepository.GetUserById(curUserId);
      if (user == null) return View("Error");
      var editUserViewModel = new EditUserDashboardViewModel()
      {
        Id = curUserId,
        Points = (int?)user.Points,
        Mileage = user.Mileage,
        ProfileImageUrl = user.ProfileImageUrl,
        City = user.City,
        State = user.State,
      };
      return View(editUserViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
    {
      if (!ModelState.IsValid)
      {
        ModelState.AddModelError("", "Failed to edit profile");
        return View("EditUserProfile", editVM);
      }
      var user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);
      if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
      {
        var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
        //Optimistic Concurrency - "Tracking error"
        // Use no tracking
        MapUserEdit(user, editVM, photoResult);

        _dashboardRepository.Update(user);
        return RedirectToAction("Index");

      }
      else
      {
        //TryCatchFinally
        try
        {
          await _photoService.DeletePhotoAsync(user.ProfileImageUrl);

        }
        catch (Exception ex)
        {
          ModelState.AddModelError("", "Failed to delete old photo");
          return View(editVM);
        }
        var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

        MapUserEdit(user, editVM, photoResult);
        _dashboardRepository.Update(user);
        return RedirectToAction("Index");

      }



    }
  }
}