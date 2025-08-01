using AutoMapper;
using MediatR;
using Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using UseCases.Activity.Queries.GetAll;
using UseCases.Activity.Queries.GetById;
using UseCases.Activity.Commands.Delete;
using Microsoft.AspNetCore.Authorization;
using UseCases.Activity.Commands.Create;
using UseCases.Activity.Commands.Update;
using UseCases.Activity.Commands.UploadImage;

namespace Controllers.Common;

[ApiController]
[AllowAnonymous]
[Route("api/activities")]
public class ActivityController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public ActivityController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Получение списка активностей
    /// </summary>
    /// <param name="filters">Набор необязательных параметров</param>
    /// <returns>Список объектов-активностей</returns>
    [HttpPost("getAll")]
    public async Task<IActionResult> GetAll([FromBody] GetActivitiesQuery filters)
    {
        var activities = await _mediator.Send(filters);
        return Ok(activities);
    }

    /// <summary>
    /// Получение активности по её идентификатору
    /// </summary>
    /// <param name="activityId">Идентификатор активности</param>
    /// <returns>Возвращает объект - активность</returns>
    [HttpGet("{activityId:guid}")]
    public async Task<IActionResult> GetByActivityId([FromRoute]Guid activityId)
    {
        return Ok(await _mediator.Send(new GetActivityByIdQuery(activityId)));
    }

    /// <summary>
    /// Создание активности
    /// </summary>
    /// <param name="createCommand">Объект-активность</param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityCommand createCommand)
    {
        await _mediator.Send(createCommand);
        return Ok();
    }

    /// <summary>
    /// Обновление активности
    /// </summary>
    /// <param name="updateCommand">Объект-активность</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateActivity([FromBody] UpdateActivityCommand updateCommand)
    {
        await _mediator.Send(updateCommand);
        return Ok();
    }
    
    /// <summary>
    /// Удаление указанных активностей
    /// </summary>
    /// <param name="activities">Объект-список активностей, подлежащих удалению</param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteActivities([FromBody]List<Guid> activities)
    {
        await _mediator.Send(new DeleteActivityCommand(activities));
        return Ok();
    }

    [HttpPost("upload/image")]
    public async Task<IActionResult> UploadImage([FromForm] UploadActivityImage uploadActivityImage)
    {
        var command = _mapper.Map<UploadActivityImageCommand>(uploadActivityImage);
        await _mediator.Send(command);
        return Ok();
    }
}