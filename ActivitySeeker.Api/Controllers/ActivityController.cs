using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Controllers;

public class ActivityController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}