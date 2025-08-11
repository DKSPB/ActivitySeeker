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
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetActivityTypesQuery()));
    }

    /// <summary>
    /// Получает тип активности по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор типа активности.</param>
    /// <returns>Возвращает объект типа активности.</returns>
    /// <response code="200">Тип активности найден и возвращён в ответе.</response>
    /// <response code="404">Тип активности с указанным идентификатором не найден.</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetActivityTypeByIdQuery(id)));
    }

    /// <summary>
    /// Создаёт новый тип активности.
    /// </summary>
    /// <param name="activityType">Данные нового типа активности.</param>
    /// <returns>Возвращает созданный тип активности.</returns>
    /// <response code="201">Тип активности успешно создан.</response>
    /// <response code="400">Неверные входные данные.</response>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateActivityTypeCommand activityType)
    {
        var newActivityType = await _mediator.Send(activityType);

        return CreatedAtAction(nameof(GetById), new { newActivityType.Id }, newActivityType);
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