using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopGroup.Data;
using WorkshopGroup.Models;
using WorkshopGroup.Repository.Interfaces;
using WorkshopGroup.Services;
using WorkshopGroup.ViewModels;

namespace WorkshopGroup.Controllers
{
    public class ClubController : Controller
  {
    //Now Bringing in through Repository// private readonly ApplicationDbContext _context;
    private readonly IClubRepository _clubRepository;
    private readonly IPhotoService _photoService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    //original// public ClubController(ApplicationDbContext context)
    public ClubController(IClubRepository clubRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
    {
      //Now Bringing in through Repository// _context = context;
      _clubRepository = clubRepository;
      _photoService = photoService;
      _httpContextAccessor = httpContextAccessor;
    }


    //orig// public IActionResult Index()
    public async Task<IActionResult> Index()
    {
      //orig// List<Club> clubs = _clubRepository.Clubs.ToList();
      IEnumerable<Club> clubs = await _clubRepository.GetAll();
      return View(clubs);
    }

    // Doing an Include() is basically a JOIN, and is a very expensive database call
    // Include is how I get the address joined onto the data that is brought back from the Club Model/DBTable
    public async Task<IActionResult> Detail(int id)
    {
      Club club = await _clubRepository.GetByIdAsync(id);
      return View(club);
    }
    public IActionResult Create()
    {
      var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
      var createClubViewModel = new CreateClubViewModel { AppUserId = curUserId };
      
        //UserId = curUserId
      
      return View(createClubViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateClubViewModel clubVM)
    {
      if (ModelState.IsValid)
      {
        var result = await _photoService.AddPhotoAsync(clubVM.Image);
        var club = new Club
        {
          Title = clubVM.Title,
          Description = clubVM.Description,
          Image = result.Url.ToString(),
          AppUserId = clubVM.AppUserId,
          Address = new Address
          {
            Street = clubVM.Address.Street,
            City = clubVM.Address.City,
            State = clubVM.Address.State,
          }
        };
        _clubRepository.Add(club);
        return RedirectToAction("Index");
      }
      else
      {
        ModelState.AddModelError("", "Photo upload failed");
      return View(clubVM);
      }
    }

    public async Task<IActionResult> Edit(int id)
    {
      var club = await _clubRepository.GetByIdAsync(id);
      if (club == null) return View("Error");
      var clubVM = new EditClubViewModel
      {
        Title = club.Title,
        Description = club.Description,
        AddressId = club.AddressId,
        Address = club.Address,
        URL = club.Image,
        ClubCategory = club.ClubCategory
      };
      return View(clubVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
    {
      if (!ModelState.IsValid)
      {
        ModelState.AddModelError("", "Failed to edit club");
        return View("Edit", clubVM);
      }
      var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

      if (userClub != null)
      {
        try
        {
          var fi = new FileInfo(userClub.Image);
          var publicId = Path.GetFileNameWithoutExtension(fi.Name);
          await _photoService.DeletePhotoAsync(publicId);
         // await _photoService.DeletePhotoAsync(userClub.Image);
        }
        catch (Exception ex)
        {
          ModelState.AddModelError("", "Could not delete photo.");
          return View(clubVM);
        }
        var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

        var club = new Club
        {
          Id = id,
          Title = clubVM.Title,
          Description = clubVM.Description,
          Image = photoResult.Url.ToString(),
          AddressId = clubVM.AddressId,
          Address = clubVM.Address,
        };

        _clubRepository.Update(club);
        return RedirectToAction("Index");
      }
      else
      {
        return View(clubVM);
      }
    }

    public async Task<IActionResult> Delete(int id)
    {
      var clubDetails = await _clubRepository.GetByIdAsync(id);
      if (clubDetails == null) return View("Error");
      return View(clubDetails);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteClub(int id)
    {
      var clubDetails = await _clubRepository.GetByIdAsync(id);
      if (clubDetails == null) return View("Error");

      _clubRepository.Delete(clubDetails); 
      return RedirectToAction("Index");
    }
    

  }
}

