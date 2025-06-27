using Controllers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Controllers.DI;
using Microsoft.AspNetCore.Authorization;

namespace Controllers.Common;

[ApiController]
[AllowAnonymous]
[Route("api/activityType")]
public class ActivityTypeController : ControllerBase
{
    /*private readonly IActivityTypeService _activityTypeService;
    
    public ActivityTypeController(IActivityTypeService activityTypeService)
    {
        _activityTypeService = activityTypeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var activityTypes = await _activityTypeService.GetAll();

        foreach (var activityType in activityTypes)
        {
            activityType.Parent = 
                activityType.ParentId is null ? null : await _activityTypeService.GetById(activityType.ParentId.Value);
        }
        
        return Ok(activityTypes.Select(x => new ActivityTypeViewModel(x)));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _activityTypeService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewActivityType activityType)
    {
        await _activityTypeService.Create(activityType.ToActivityTypeDto());
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] NewActivityType activityType)
    {
        await _activityTypeService.Update(activityType.ToActivityTypeDto());
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<Guid> activityTypeIds)
    {
        await _activityTypeService.Delete(activityTypeIds);
        return Ok();
    }

    [HttpPost("upload/image")]
    public async Task<IActionResult> UploadActivityTypeImage(
        [FromServices]IWebHostEnvironment webHostEnvironment, 
        [FromServices]IOptions<FileInfoOption> fileInfoOption, 
        [FromForm] ActivityTypeImage activityTypeImage)
    {
        var maxFileSize = fileInfoOption.Value.MaxFileSize;
        var fileSize = activityTypeImage.File.Length;

        if (FileProvider.ValidateFileSize(fileSize, maxFileSize))
        {
            var webRootPath = webHostEnvironment.WebRootPath;
            var rootImageFolder = fileInfoOption.Value.RootImageFolder;
            var newFilename = Path.GetRandomFileName();

            var fullPath = FileProvider.CombinePathToFile(webRootPath, rootImageFolder, newFilename);

            await using (Stream imageStream = activityTypeImage.File.OpenReadStream())
                await _activityTypeService.UploadImage(activityTypeImage.ActivityTypeId, fullPath, imageStream);

            return Ok();
        }

        return BadRequest($"������ ������������ ����� ��������� {maxFileSize / (1024 * 1024)} ��");
        
    }*/
}