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
    private const string Folder = "files";
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
        if (string.IsNullOrEmpty(fileUploader.File.FileName))
        {
            return BadRequest();
        }
        
        var fileName = _settings.TelegramBotSettings.MainMenuImageName;
        var filePath = Path.Combine(_hostEnvironment.WebRootPath, Folder, fileName);

        await _settingsService.UploadImage(filePath, fileUploader.File.OpenReadStream());

        return Ok();
    }

    [HttpGet("get/tg/main_menu/img")]
    public async Task<IActionResult> GetMainMenuImage()
    {
        var path = Path.Combine(_hostEnvironment.WebRootPath, Folder, _settings.TelegramBotSettings.MainMenuImageName);
        return Ok(await _settingsService.GetImage(path));
    }

    [HttpPost("upload/tg/offer_menu/img")]
    public async Task<IActionResult> UploadOfferMenuImage([FromForm] FileUploader fileUploader)
    {
        if (string.IsNullOrEmpty(fileUploader.File.FileName))
        {
            return BadRequest();
        }
        
        var fileName = _settings.TelegramBotSettings.OfferMenuImageName;
        var filePath = Path.Combine(_hostEnvironment.WebRootPath, Folder, fileName);

        await _settingsService.UploadImage(filePath, fileUploader.File.OpenReadStream());

        return Ok();
    }
    
    public class FileUploader
    {
        public IFormFile File { get; set; }
    }
}