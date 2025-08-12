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
[Route("api/activityTypes")]
public class ActivityTypeController : ControllerBase
{
    private readonly IMediator _mediator;
    public ActivityTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int limit = 20, int offset = 1)
    {
        return Ok(await _mediator.Send(new GetActivityTypesQuery(limit, offset)));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetActivityTypeByIdQuery(id)));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateActivityTypeCommand activityType)
    {
        var newActivityType = await _mediator.Send(activityType);

        return CreatedAtAction(nameof(GetById), new { Id = newActivityType.Id }, newActivityType);
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateActivityTypeCommand activityType)
    {
        var updatedActivityType = await _mediator.Send(activityType);
        return Ok(updatedActivityType);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteActivityTypeCommand(id));
        return NoContent();
    }
}