using ActivitySeeker.Bll.Utils;
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
    private readonly BotConfiguration _botConfig;
    private readonly string _webRootPath;
   
    public SettingsController(IWebHostEnvironment hostEnvironment, IOptions<BotConfiguration> botConfigOptions)
    {
        _botConfig = botConfigOptions.Value;
        _webRootPath = hostEnvironment.WebRootPath;
    }

    [HttpPost("upload/state/img")]
    public async Task<IActionResult> UploadStateImage([FromForm] FileUploader fileUploader)
    {
        if (!FileProvider.ValidateFileSize(fileUploader.File.Length, _botConfig.MaxFileSize) || 
            !FileProvider.ValidateFileNameIsNotNull(fileUploader.File.FileName))
        {
            return BadRequest();
        }
        
        var stateName = fileUploader.State.ToString();

        var path = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, stateName);

        await using (var stream = fileUploader.File.OpenReadStream())
            await FileProvider.UploadImage(path, stream);

        return Ok();
    }

    [HttpGet("get/state/img")]
    public async Task<IActionResult> GetStateImage([FromQuery]StatesEnum state)
    {
        var fileName = state.ToString();
        var path = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return Ok(await FileProvider.GetImage(path));
    }

    public class FileUploader
    {
        public StatesEnum State { get; set; }
        public IFormFile File { get; set; } = default!;
    }
}