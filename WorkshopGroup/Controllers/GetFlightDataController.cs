using Microsoft.AspNetCore.Mvc;

namespace FlightApplication.Controllers;

public class FormController : Controller
{

    [HttpPost]
    public IActionResult Index(string name)
    {
        return Content($"Hello {name}");
    }    
}