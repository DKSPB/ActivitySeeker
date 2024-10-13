using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Requests.Abstractions;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[Route("api/activity")]
public class ActivityController : ControllerBase
{
    private readonly IActivityService _activityService;
    private readonly NewActivityValidator _newActivityValidator;

    public ActivityController(IActivityService activityService, NewActivityValidator newActivityValidator)
    {
        _activityService = activityService;
        _newActivityValidator = newActivityValidator;
    }
    
    /// <summary>
    /// Получение списка активностей
    /// </summary>
    /// <param name="request">Набор необязательных параметров</param>
    /// <returns>Список объектов-активностей</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ActivityFilters filters)
    {
        var activities = _activityService.GetActivities(filters.ActivityRequest);

        if (activities == null) 
        {
            return Ok(new PageDto<ActivityBaseDto>());
        }

        var total = activities.Count();

        var data = await activities
            .Skip(filters.Offset)
            .Take(filters.Limmit)
            .Select(x => new ActivityBaseDto(x))
            .ToListAsync();

        return Ok(new PageDto<ActivityBaseDto>
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
    public async Task<IActionResult> DeleteActivities(List<ActivityDto> activities)
    {
        await _activityService.DeleteActivity(activities);
        return Ok();
    }
    
}