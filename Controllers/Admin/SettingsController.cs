using ActivitySeeker.Bll.Utils;
using Controllers.DI;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Controllers.Admin;

[ApiController]
[AllowAnonymous]
[Route("api/settings")]
public class SettingsController : ControllerBase
{
    private readonly FileInfoOption _fileInfoOption;
    private readonly string _webRootPath;
   
    public SettingsController(IWebHostEnvironment hostEnvironment, IOptions<FileInfoOption> fileInfoOption)
    {
        _fileInfoOption = fileInfoOption.Value;
        _webRootPath = hostEnvironment.WebRootPath;
    }

    [HttpGet("states")]
    public IActionResult GetStates()
    {
        return Ok((StatesEnum[])Enum.GetValues(typeof(StatesEnum)));
    }

    [HttpPost("states/upload/img")]
    public async Task<IActionResult> UploadStateImage([FromForm] FileUploader fileUploader)
    {
        if (!FileProvider.ValidateFileSize(fileUploader.File.Length, _fileInfoOption.MaxFileSize) || 
            !FileProvider.ValidateFileNameIsNotNull(fileUploader.File.FileName))
        {
            return BadRequest();
        }
        
        var stateName = fileUploader.State.ToString();

        var path = FileProvider.CombinePathToFile(_webRootPath, _fileInfoOption.RootImageFolder, stateName);

        await using (var stream = fileUploader.File.OpenReadStream())
            await FileProvider.UploadImage(path, stream);

        return Ok();
    }

    [HttpGet("states/get/img")]
    public async Task<IActionResult> GetStateImage([FromQuery]StatesEnum state)
    {
        var fileName = state.ToString();
        var path = FileProvider.CombinePathToFile(_webRootPath, _fileInfoOption.RootImageFolder, fileName);

        return Ok(await FileProvider.GetImage(path));
    }

    public class FileUploader
    {
        public StatesEnum State { get; set; }
        public IFormFile File { get; set; } = default!;
    }
}