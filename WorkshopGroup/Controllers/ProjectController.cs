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

    public ProjectController(IProjectRepository projectRepository, IPhotoService photoService)
    {
      _projectRepository = projectRepository;
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

    public async Task<IActionResult> Edit(int id)
    {
      var project = await _projectRepository.GetByIdAsync(id);
      if (project == null) return View("Error");
      var projectVM = new EditProjectViewModel
      {
        Title = project.Title,
        Description = project.Description,
        AddressId = project.AddressId,
        Address = project.Address,
        URL = project.Image,
        ProjectCategory = project.ProjectCategory
      };
      return View(projectVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditProjectViewModel projectVM)
    {
      if (!ModelState.IsValid)
      {
        ModelState.AddModelError("", "Failed to edit club");
        return View("Edit", projectVM);
        // return View(projectVM);
      }
      var userProject = await _projectRepository.GetByIdAsyncNoTracking(id);

      if (userProject != null)
      {
        /*return View("Error");
      }

      var photoResult = await _photoService.AddPhotoAsync(projectVM.Image);

      if (photoResult.Error != null)
      {
        ModelState.AddModelError("Image", "Photo upload failed");
        return View(projectVM);
      }

      if (!string.IsNullOrEmpty(userProject.Image))
      {
        _ = _photoService.DeletePhotoAsync(userProject.Image);
      }

      var project = new Project
      {
        Id = id,
        Title = projectVM.Title,
        Description = projectVM.Description,
        Image = photoResult.Url.ToString(),
        AddressId = projectVM.AddressId,
        Address = projectVM.Address,
      };

      _projectRepository.Update(project);

      return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
      var projectDetails = await _projectRepository.GetByIdAsync(id);
      if (projectDetails == null) return View("Error");
      return View(projectDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteClub(int id)
    {
      var projectDetails = await _projectRepository.GetByIdAsync(id);

      if (projectDetails == null)
      {
        return View("Error");
      }

      if (!string.IsNullOrEmpty(projectDetails.Image))
      {
        _ = _photoService.DeletePhotoAsync(projectDetails.Image);
      }

      _projectRepository.Delete(projectDetails);
      return RedirectToAction("Index");
    }
  }
}*/


        try
        {
          /*var fi = new FileInfo(userProject.Image);
          var publicId = Path.GetFileNameWithoutExtension(fi.Name);
          await _photoService.DeletePhotoAsync(publicId);
*/
           await _photoService.DeletePhotoAsync(userProject.Image);
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("", "Could not delete photo.");
          return View(projectVM);
        }
        var photoResult = await _photoService.AddPhotoAsync(projectVM.Image);

        var project = new Project
        {
          Id = id,
          Title = projectVM.Title,
          Description = projectVM.Description,
          Image = photoResult.Url.ToString(),
          AddressId = projectVM.AddressId,
          Address = projectVM.Address,
        };

        _projectRepository.Update(project);
        return RedirectToAction("Index");
      }
      else
      {
        return View(projectVM);
      }

    }


  }
}

