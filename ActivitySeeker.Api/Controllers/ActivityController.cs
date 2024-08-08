using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[Route("api/activity")]
public class ActivityController : ControllerBase
{
    private readonly IActivityService _activityService;
    
    public ActivityController(IActivityService activityService)
    {
        _activityService = activityService;
    }
    
    [HttpGet("get/all")]
    public IActionResult GetAll([FromQuery] State state)
    {
        return Ok(_activityService.GetActivities(state));
    }

    /// <summary>
    /// Получение активности по её идентификатору
    /// </summary>
    /// <param name="activityId">Идентификатор активности</param>
    /// <returns>Возвращает объект - активность</returns>
    [HttpGet("{activityId:guid}/get")]
    public async Task<IActionResult> GetByActivityId([FromRoute]Guid activityId)
    {
        return Ok(await _activityService.GetActivity(activityId));
    }

    /// <summary>
    /// Получение списка активностей заданного типа
    /// </summary>
    /// <param name="activityTypeId">Идентификатор типа активности</param>
    /// <returns>Список активностей</returns>
    [HttpGet("{activityTypeId:guid}/get")]
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
    public async Task<IActionResult> CreateActivity([FromForm] ActivityDto activity)
    {
        await _activityService.CreateActivity(activity);
        return Ok();
    }

    /// <summary>
    /// Обновление активности
    /// </summary>
    /// <param name="activity">Объект-активность</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateActivity([FromForm] ActivityDto activity)
    {
        await _activityService.UpdateActivity(activity);
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