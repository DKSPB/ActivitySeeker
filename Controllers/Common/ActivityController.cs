using MediatR;
using AutoMapper;
using Controllers.Utils;
using Controllers.Models;
using Microsoft.AspNetCore.Mvc;
using UseCases.Activity.Queries.GetAll;
using UseCases.Activity.Queries.GetById;
using UseCases.Activity.Commands.Delete;
using UseCases.Activity.Commands.Create;
using UseCases.Activity.Commands.Update;
using UseCases.Activity.Queries.GetImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using UseCases.Activity.Commands.Publish;
using UseCases.Activity.Commands.Unpublish;
using UseCases.Activity.Commands.UploadImage;
using UseCases.Activity.Queries.GetImage.Models;

namespace Controllers.Common;

[ApiController]
[AllowAnonymous]
[Route("api/activities")]
public class ActivityController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public ActivityController(IMediator mediator, IMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <summary>
    /// Получение списка активностей с фильтрацией и пагинацией
    /// </summary>
    /// <param name="filters">Объект с фильтрами и параметрами пагинации</param>
    /// <returns>Список объектов-активностей</returns>
    [HttpPost("getAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromBody] GetActivitiesQuery filters)
    {
        var activities = await _mediator.Send(filters);
        return Ok(activities);
    }

    /// <summary>
    /// Получение активности по её идентификатору
    /// </summary>
    /// <param name="activityId">GUID идентификатор активности</param>
    /// <returns>Возвращает объект активности</returns>
    [HttpGet("{activityId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByActivityId([FromRoute] Guid activityId)
    {
        return Ok(await _mediator.Send(new GetActivityByIdQuery(activityId)));
    }

    /// <summary>
    /// Создание новой активности
    /// </summary>
    /// <param name="command">Объект с данными новой активности</param>
    /// <returns>Созданный объект активности с его идентификатором</returns>
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityCommand command)
    {
        var newActivity = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByActivityId), new { id = newActivity.Id}, newActivity);
    }

    /// <summary>
    /// Обновление активности
    /// </summary>
    /// <param name="id">GUID идентификатор обновляемой активности</param>
    /// <param name="command">Объект с новыми данными активности</param>
    /// <returns>Обновлённый объект активности</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateActivity([FromRoute] Guid id, [FromBody] CreateActivityCommand command)
    {
        var updateCommand = _mapper.Map<UpdateActivityCommand>(command);
        updateCommand.Id = id;
        
        return Ok(await _mediator.Send(command));
    }

    /// <summary>
    /// Удаление списка активностей
    /// </summary>
    /// <param name="activityId">Идентификатор активности, которую нужно удалить</param>
    /// <returns>Статус успешного удаления</returns>
    [HttpDelete("{activityId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteActivities(Guid activityId)
    {
        await _mediator.Send(new DeleteActivityCommand(activityId));
        return NoContent();
    }

    /// <summary>
    /// Загрузка изображения для активности
    /// </summary>
    /// <param name="uploadActivityImage">Данные загружаемого изображения</param>
    /// <returns>Статус успешной загрузки</returns>
    [HttpPost("upload/image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadImage([FromForm] UploadActivityImage uploadActivityImage)
    {
        var command = _mapper.Map<UploadActivityImage, UploadActivityImageCommand>(uploadActivityImage);
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Получение изображения активности по идентификатору и размеру
    /// </summary>
    /// <param name="activityId">GUID идентификатор активности</param>
    /// <param name="imageSize">Размер изображения (query-параметр)</param>
    /// <returns>Файл изображения или 404, если не найден</returns>
    [HttpGet("{activityId:guid}/image")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImage(Guid activityId, [FromQuery]ImageSize imageSize = ImageSize.Medium)
    {
        var fileResult = await _mediator.Send(new GetImageCommand(activityId, imageSize));

        if (fileResult is null)
            return NotFound();

        var mimeType = MimeTypeExtractor.GetMimeTypeByExtension(fileResult.Extension);

        return File(fileResult.Content, mimeType);
    }
    
    /// <summary>
    /// Публикация активности
    /// </summary>
    /// <param name="activityId">GUID идентификатор активности</param>
    /// <returns>Статус успешной публикации</returns>
    [HttpPatch("{activityId:guid}/publish")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Publish(Guid activityId)
    {
        await _mediator.Send(new PublishActivityCommand(activityId));
        return NoContent();
    }

    /// <summary>
    /// Снятие публикации активности
    /// </summary>
    /// <param name="activityId">GUID идентификатор активности</param>
    /// <returns>Статус успешного снятия публикации</returns>
    [HttpPatch("{activityId:guid}/unPublish")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UnPublish(Guid activityId)
    {
        await _mediator.Send(new UnpublishActivityCommand(activityId));
        return NoContent();
    }
}