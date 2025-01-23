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
    private readonly string _webRootPath;
    private readonly Settings _settings;
    private readonly ISettingsService _settingsService;
   
    public SettingsController(IWebHostEnvironment hostEnvironment, IOptions<Settings> settingsOption, ISettingsService settingsService)
    {
        _webRootPath = hostEnvironment.WebRootPath;
        _settingsService = settingsService;
        _settings = settingsOption.Value;
    }

    [HttpPost("upload/tg/main_menu/img")]
    public async Task<IActionResult> UploadTgMainMenuImage([FromForm] FileUploader fileUploader)
    {
        if (string.IsNullOrEmpty(fileUploader.File.FileName))
        {
            return BadRequest();
        }

        var fileName = _settings.TelegramBotSettings.MainMenuImageName;
        var path = _settingsService.CombinePathToFile(_webRootPath, fileName);
        await _settingsService.UploadImage(path, fileUploader.File.OpenReadStream());

        return Ok();
    }

    [HttpGet("get/tg/main_menu/img")]
    public async Task<IActionResult> GetTgMainMenuImage()
    {
        var fileName = _settings.TelegramBotSettings.MainMenuImageName;
        var path = _settingsService.CombinePathToFile(_webRootPath, fileName);

        return Ok(await _settingsService.GetImage(path));
    }

    [HttpPost("upload/tg/offer_menu/img")]
    public async Task<IActionResult> UploadTgOfferMenuImage([FromForm] FileUploader fileUploader)
    {
        if (string.IsNullOrEmpty(fileUploader.File.FileName))
        {
            return BadRequest();
        }

        var fileName = _settings.TelegramBotSettings.OfferMenuImageName;
        var path = _settingsService.CombinePathToFile(_webRootPath, fileName);
        await _settingsService.UploadImage(path, fileUploader.File.OpenReadStream());

        return Ok();
    }

    [HttpGet("get/tg/offer_menu/img")]
    public async Task<IActionResult> GetTgOfferMenuImage()
    {
        var fileName = _settings.TelegramBotSettings.OfferMenuImageName;
        var path = _settingsService.CombinePathToFile(_webRootPath, fileName);

        return Ok(await _settingsService.GetImage(path));
    }

    public class FileUploader
    {
        public IFormFile File { get; set; } = default!;
    }
}