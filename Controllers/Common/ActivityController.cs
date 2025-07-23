using MediatR;
using Microsoft.AspNetCore.Mvc;
using UseCases.Activity.Queries.GetAll;
using UseCases.Activity.Queries.GetById;
using UseCases.Activity.Commands.Delete;
using Microsoft.AspNetCore.Authorization;

namespace Controllers.Common;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class ActivityController : ControllerBase
{
    private readonly IMediator _mediator;
    public ActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение списка активностей
    /// </summary>
    /// <param name="filters">Набор необязательных параметров</param>
    /// <returns>Список объектов-активностей</returns>
    [HttpPost]
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

    /*/// <summary>
    /// Создание активности
    /// </summary>
    /// <param name="createCommand">Объект-активность</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromForm] CreateActivityCommand createCommand)
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
    public async Task<IActionResult> UpdateActivity([FromForm] UpdateActivityCommand updateCommand)
    {
        await _mediator.Send(updateCommand);
        return Ok();
    }*/
    
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

    /*/// <summary>
    /// Публикация активностей
    /// </summary>
    /// <param name="activityIds">Идентификаторы активностей</param>
    /// <returns></returns>
    [HttpPut("publish")]
    public async Task<IActionResult> PublishActivities([FromBody] List<Guid> activityIds)
    {
        if (activityIds is not null && activityIds.Count > 0) 
        {
            foreach (var id in activityIds)
            {
                var activity = await _activityService.GetActivityAsync(id);

                if (activity is not null)
                {
                    var responseMessage = new ResponseMessage
                    {
                        Text = activity.GetActivityDescription().ToString(),
                        Image = activity.Image,
                        Keyboard = InlineKeyboardMarkup.Empty()
                    };
                    var tgMessage = await _activityPublisher.SendMessageAsync(_botConfig.TelegramChannel, responseMessage);

                    await _activityService.PublishActivity(activity, tgMessage.MessageId);
                }
            }
        }

        return Ok();
    }*/

    /*[HttpPut("withdraw")]
    public async Task<IActionResult> WithdrawFromPublication([FromBody] List<Guid> activityIds)
    {
        if (activityIds is not null && activityIds.Count > 0)
        {
            foreach (var id in activityIds)
            {
                var activity = await _activityService.GetActivityAsync(id);

                if (activity is not null && activity.TgMessageId.HasValue)
                {
                    await _activityPublisher.DeleteMessage(_botConfig.TelegramChannel, activity.TgMessageId.Value);
                    await _activityService.WithdrawFromPublication(activity);
                }
            }
        }

        return Ok();
    }*/
}