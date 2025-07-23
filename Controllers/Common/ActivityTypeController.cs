using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UseCases.ActivityType.Queries.GetAll;
using UseCases.ActivityType.Commands.Create;
using UseCases.ActivityType.Commands.Delete;
using UseCases.ActivityType.Commands.Update;
using UseCases.ActivityType.Queries.GetById;

namespace Controllers.Common;

[ApiController]
[AllowAnonymous]
[Route("api/activityType")]
public class ActivityTypeController : ControllerBase
{
    private readonly IMediator _mediator;
    public ActivityTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetActivityTypesQuery()));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetActivityTypeByIdQuery(id)));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateActivityTypeCommand activityType)
    {
        await _mediator.Send(activityType);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateActivityTypeCommand activityType)
    {
        await _mediator.Send(activityType);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<Guid> activityTypeIds)
    {
        await _mediator.Send(new DeleteActivityTypeCommand(activityTypeIds));
        return Ok();
    }
}