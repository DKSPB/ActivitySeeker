using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Infrastructure.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/activity")]
public class SettingsController : ControllerBase
{
    private const string Folder = "/Files/";
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly Settings _settings;
    private readonly ISettingsService _settingsService;
   
    public SettingsController(IWebHostEnvironment hostEnvironment, IOptions<Settings> settingsOption, ISettingsService settingsService)
    {
        _hostEnvironment = hostEnvironment;
        _settingsService = settingsService;
        _settings = settingsOption.Value;
    }

    [HttpPost("upload/tg/main_menu/img")]
    public async Task<IActionResult> UploadMainMenuImage([FromForm] FileUploader fileUploader)
    {
        var filePath =Path.Combine(_hostEnvironment.WebRootPath, Folder);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await fileUploader.File.CopyToAsync(stream);
        
        return Ok();
    }

    /*[HttpPost]
    public IActionResult UploadOfferMenuImage([FromForm] FileUploader fileUploader)
    {
        return Ok();
    }*/
    
    public class FileUploader
    {
        public IFormFile File { get; set; }
    }
}