using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/activity")]
public class SettingsController : ControllerBase
{
    public SettingsController()
    {
        
    }

    [HttpPost]
    public IActionResult UploadMainMenuImage([FromForm] IFormFile image)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult UploadOfferMenuImage([FromForm] IFormFile image)
    {
        return Ok();
    }
}