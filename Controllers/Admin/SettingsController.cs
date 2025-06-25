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
}