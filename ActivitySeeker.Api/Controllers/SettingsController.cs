using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
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
    private readonly BotConfiguration _botConfig;
    private readonly ISettingsService _settingsService;
   
    public SettingsController(IWebHostEnvironment hostEnvironment, IOptions<BotConfiguration> botConfigOptions, ISettingsService settingsService)
    {
        _webRootPath = hostEnvironment.WebRootPath;
        _settingsService = settingsService;
        _botConfig = botConfigOptions.Value;
    }

    [HttpPost("upload/state/img")]
    public async Task<IActionResult> UploadTgMainMenuImage([FromForm] FileUploader fileUploader)
    {
        if (string.IsNullOrEmpty(fileUploader.File.FileName))
        {
            return BadRequest();
        }

        var fileName = fileUploader.State.ToString();
        var path = _settingsService.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);
        await _settingsService.UploadImage(path, fileUploader.File.OpenReadStream());

        return Ok();
    }

    [HttpGet("get/state/img")]
    public async Task<IActionResult> GetTgMainMenuImage([FromQuery]StatesEnum state)
    {
        var fileName = state.ToString();
        var path = _settingsService.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return Ok(await _settingsService.GetImage(path));
    }

    public class FileUploader
    {
        public StatesEnum State { get; set; }
        public IFormFile File { get; set; } = default!;
    }
}