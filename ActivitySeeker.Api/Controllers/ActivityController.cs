using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.TelegramBot;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/activity")]
public class ActivityController : ControllerBase
{
    private readonly IActivityService _activityService;
    private readonly NewActivityValidator _newActivityValidator;
    private readonly ActivityPublisher _activityPublisher;
    private readonly BotConfiguration _botConfig;

    public ActivityController(IActivityService activityService, NewActivityValidator newActivityValidator, ActivityPublisher activityPublisher, IOptions<BotConfiguration> botOptions)
    {
        _activityService = activityService;
        _newActivityValidator = newActivityValidator;
        _activityPublisher = activityPublisher;
        _botConfig = botOptions.Value;
    }

    /// <summary>
    /// Получение списка активностей
    /// </summary>
    /// <param name="filters">Набор необязательных параметров</param>
    /// <returns>Список объектов-активностей</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ActivityFilters filters)
    {
        var activities = _activityService.GetActivities(filters.ActivityRequest);

        if (activities == null) 
        {
            return Ok(new PageDto<ActivityViewModel>());
        }

        var total = activities.Count();

        var data = await activities
            .Where(y => y.IsPublished != null)
            .Include(x => x.ActivityType)
            .Include(z => z.ActivityCity)
            .OrderByDescending(x => x.StartDate)
            .Skip(filters.Offset)
            .Take(filters.Limit)
            .Select(x => new ActivityViewModel(x))
            .ToListAsync();

        return Ok(new PageDto<ActivityViewModel>
        {
            Total = total,
            Data = data
        });
    }

    /// <summary>
    /// Получение активности по её идентификатору
    /// </summary>
    /// <param name="activityId">Идентификатор активности</param>
    /// <returns>Возвращает объект - активность</returns>
    [HttpGet("{activityId:guid}")]
    public async Task<IActionResult> GetByActivityId([FromRoute]Guid activityId)
    {
        return Ok(await _activityService.GetActivityAsync(activityId));
    }

    /// <summary>
    /// Получение списка активностей заданного типа
    /// </summary>
    /// <param name="activityTypeId">Идентификатор типа активности</param>
    /// <returns>Список активностей</returns>
    [HttpGet("type/{activityTypeId:guid}")]
    public async Task<IActionResult> GetByActivitiesTypeId([FromRoute]Guid activityTypeId)
    {
        return Ok(await _activityService.GetActivitiesByType(activityTypeId));
    }

    /// <summary>
    /// Создание активности
    /// </summary>
    /// <param name="activity">Объект-активность</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromForm] NewActivity activity)
    {
        var validationResult = await _newActivityValidator.ValidateAsync(activity);

        if (!validationResult.IsValid)
        {
            return StatusCode(StatusCodes.Status400BadRequest, validationResult.Errors);
        }

        await _activityService.CreateActivity(activity.ToActivityDto());
        return Ok();
    }

    /// <summary>
    /// Обновление активности
    /// </summary>
    /// <param name="activity">Объект-активность</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateActivity([FromForm] NewActivity activity)
    {
        await _activityService.UpdateActivity(activity.ToActivityDto());
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
        await _activityService.DeleteActivity(activities);
        return Ok();
    }

    /// <summary>
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
    }

    [HttpPut("withdraw")]
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
    }
}