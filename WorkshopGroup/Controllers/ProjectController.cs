using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopGroup.Data;
using WorkshopGroup.Interfaces;
using WorkshopGroup.Models;
using WorkshopGroup.Repository;
using WorkshopGroup.Services;
using WorkshopGroup.ViewModels;

namespace WorkshopGroup.Controllers
{
  public class ProjectController : Controller
  {
    private readonly IProjectRepository _projectRepository;
    private readonly IPhotoService _photoService;

    public ProjectController(IProjectRepository projectorRepository, IPhotoService photoService)
    {
      _projectRepository = projectorRepository;
      _photoService = photoService;
    }
    public async Task<IActionResult> Index()
    {
      IEnumerable<Project> projects = await _projectRepository.GetAll();
      return View(projects);
    }

    public async Task<IActionResult> Detail(int id)
    {
      Project project = await _projectRepository.GetByIdAsync(id);
      return View(project);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectViewModel projectVM)
    {
      if (ModelState.IsValid)
      {
        var result = await _photoService.AddPhotoAsync(projectVM.Image);
        var project = new Project
        {
          Title = projectVM.Title,
          Description = projectVM.Description,
          Image = result.Url.ToString(),
          Address = new Address
          {
            Street = projectVM.Address.Street,
            City = projectVM.Address.City,
            State = projectVM.Address.State,
          }
        };
        _projectRepository.Add(project);
        return RedirectToAction("Index");
      }
      else
      {
        ModelState.AddModelError("", "Photo upload failed");
      }
      return View(projectVM);
    }
  }
}
