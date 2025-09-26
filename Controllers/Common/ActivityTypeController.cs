using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using UseCases.ActivityType.Queries.GetAll;
using UseCases.ActivityType.Commands.Create;
using UseCases.ActivityType.Commands.Delete;
using UseCases.ActivityType.Commands.Update;
using UseCases.ActivityType.Models;
using UseCases.ActivityType.Queries.GetById;

namespace Controllers.Common;

/// <summary>
/// Управление типами активностей.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("api/activityTypes")]
public class ActivityTypeController : ControllerBase
{
    private readonly IMediator _mediator;
    
    /// <summary>
    /// Создаёт экземпляр контроллера типов активностей.
    /// </summary>
    /// <param name="mediator">MediatR для отправки команд и запросов.</param>
    public ActivityTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список всех типов активностей с поддержкой пагинации.
    /// </summary>
    /// <param name="limit">Количество записей, возвращаемых за один запрос (по умолчанию 20).</param>
    /// <param name="offset">Смещение относительно начала выборки (по умолчанию 1).</param>
    /// <returns>Список объектов <see cref="ActivityTypeDto"/>.</returns>
    /// <response code="200">Список успешно получен.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ActivityTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(int limit = 20, int offset = 1)
    {
        return Ok(await _mediator.Send(new GetActivityTypesQuery(limit, offset)));
    }
    
    /// <summary>
    /// Получить тип активности по идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор типа активности.</param>
    /// <returns>Объект <see cref="ActivityTypeDto"/>.</returns>
    /// <response code="200">Тип активности найден.</response>
    /// <response code="404">Тип активности с указанным ID не найден.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ActivityTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _mediator.Send(new GetActivityTypeByIdQuery(id)));
    }
    
    /// <summary>
    /// Создать новый тип активности.
    /// </summary>
    /// <param name="activityType">Данные нового типа активности.</param>
    /// <returns>Созданный объект <see cref="ActivityTypeDto"/> с присвоенным идентификатором.</returns>
    /// <response code="201">Тип активности успешно создан.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ActivityTypeDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateActivityTypeCommand activityType)
    {
        var newActivityType = await _mediator.Send(activityType);

        return CreatedAtAction(nameof(GetById), new { Id = newActivityType.Id }, newActivityType);
    }

    /// <summary>
    /// Обновить существующий тип активности.
    /// </summary>
    /// <param name="activityType">Обновлённые данные типа активности.</param>
    /// <returns>Обновлённый объект <see cref="ActivityTypeDto"/>.</returns>
    /// <response code="200">Тип активности успешно обновлён.</response>
    /// <response code="404">Тип активности не найден.</response>
    [HttpPatch]
    [ProducesResponseType(typeof(ActivityTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateActivityTypeCommand activityType)
    {
        var updatedActivityType = await _mediator.Send(activityType);
        return Ok(updatedActivityType);
    }

    /// <summary>
    /// Удалить тип активности по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого типа активности.</param>
    /// <response code="204">Тип активности успешно удалён.</response>
    /// <response code="404">Тип активности не найден.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteActivityTypeCommand(id));
        return NoContent();
    }
}