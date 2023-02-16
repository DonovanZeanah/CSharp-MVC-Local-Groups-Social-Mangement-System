using Microsoft.AspNetCore.Mvc;
using WorkshopGroup.Interfaces;

namespace WorkshopGroup.Controllers
{
  public class SkillController : Controller
  {
    private readonly ISkillRepository _skillRepository;
    private readonly IPhotoService _photoService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public SkillController(ISkillRepository skillRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
    {
    } 
    public IActionResult Index()
    {
      return View();
    }
  }
}
