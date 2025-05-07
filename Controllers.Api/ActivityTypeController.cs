using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ActivitySeeker.UseCases.ActivityType.Dto;
using ActivitySeeker.UseCases.ActivityType.Queries.GetAll;
using ActivitySeeker.UseCases.ActivityType.Queries.GetById;
using ActivitySeeker.UseCases.ActivityType.Commands.CreateActivityType;
using ActivitySeeker.UseCases.ActivityType.Commands.DeleteActivityType;
using ActivitySeeker.UseCases.ActivityType.Commands.UpdateActivityType;
using ActivitySeeker.UseCases.ActivityType.Commands.UploadActivityTypeImage;
using Controllers.Api.Models;

namespace Controllers.Api;

[ApiController]
[AllowAnonymous]
[Route("api/activityType")]
public class ActivityTypeController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ActivityTypeController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
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

    [HttpPost("upload/image")]
    public async Task<IActionResult> UploadActivityTypeImage([FromForm] ActivityTypeImageVM activityTypeImageVm)
    {
        /*if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }*/
        
        var imageDto = _mapper.Map<UploadActivityTypeImageDto>(activityTypeImageVm);
        
        var uploadActivityTypeImageCommand = new UploadActivityTypeImageCommand(imageDto);
        
        await _sender.Send(uploadActivityTypeImageCommand);

        return Ok();
    }
}