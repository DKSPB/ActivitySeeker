using ActivitySeeker.UseCases.ActivityType.Commands.CreateActivityType;
using ActivitySeeker.UseCases.ActivityType.Commands.DeleteActivityType;
using ActivitySeeker.UseCases.ActivityType.Commands.UpdateActivityType;
using ActivitySeeker.UseCases.ActivityType.Dto;
using ActivitySeeker.UseCases.ActivityType.Queries.GetAll;
using ActivitySeeker.UseCases.ActivityType.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/activityType")]
public class ActivityTypeController : ControllerBase
{
    private readonly ISender _sender;

    public ActivityTypeController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var getAllCommand = new GetAllActivityTypeQuery();
        
        return Ok(await _sender.Send(getAllCommand));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var getByIdQuery = new GetActivityTypeByIdQuery(id);
        
        return Ok(await _sender.Send(getByIdQuery));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateActivityTypeDto activityType)
    {
        var createCommand = new CreateActivityTypeCommand(activityType);
        await _sender.Send(createCommand);
        
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateActivityTypeDto activityType)
    {
        var updateCommand = new UpdateActivityTypeCommand(activityType);
        await _sender.Send(updateCommand);
        
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<Guid> activityTypeIds)
    {
        var deleteCommand = new DeleteActivityTypeCommand(activityTypeIds);
        await _sender.Send(deleteCommand);
        
        return Ok();
    }

    //[HttpPost("upload/image")]
    /*public async Task<IActionResult> UploadActivityTypeImage(
        [FromServices]IWebHostEnvironment webHostEnvironment, 
        [FromServices]IOptions<BotConfiguration> botConfigOptions, 
        [FromForm] ActivityTypeImage activityTypeImage)
    {
        var maxFileSize = botConfigOptions.Value.MaxFileSize;
        var fileSize = activityTypeImage.File.Length;

        if (FileProvider.ValidateFileSize(fileSize, maxFileSize))
        {
            var webRootPath = webHostEnvironment.WebRootPath;
            var rootImageFolder = botConfigOptions.Value.RootImageFolder;
            var newFilename = Path.GetRandomFileName();

            var fullPath = FileProvider.CombinePathToFile(webRootPath, rootImageFolder, newFilename);

            await using (Stream imageStream = activityTypeImage.File.OpenReadStream())
                await _activityTypeService.UploadImage(activityTypeImage.ActivityTypeId, fullPath, imageStream);

            return Ok();
        }

        return BadRequest($"������ ������������ ����� ��������� {maxFileSize / (1024 * 1024)} ��");
        
    }*/
}