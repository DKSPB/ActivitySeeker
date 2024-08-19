using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[Route("api/activityType")]
public class ActivityTypeController : ControllerBase
{
    private readonly IActivityTypeService _activityTypeService;
    
    public ActivityTypeController(IActivityTypeService activityTypeService)
    {
        _activityTypeService = activityTypeService;
    }
    
    /// <summary>
    /// Получение списка всех типов активностей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _activityTypeService.GetTypes());
    }

    /// <summary>
    /// Получение типа активности по заданному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор активности</param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _activityTypeService.GetById(id));
    }

    /// <summary>
    /// Создание нового типа активности
    /// </summary>
    /// <param name="activityType">Объект-активность</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ActivityTypeDto activityType)
    {
        await _activityTypeService.Create(activityType);
        return Ok();
    }

    /// <summary>
    /// Обеовление выбранной активности
    /// </summary>
    /// <param name="activityType">Объект-активность</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ActivityTypeDto activityType)
    {
        await _activityTypeService.Update(activityType);
        return Ok();
    }

    /// <summary>
    /// Удалкение списка выбранных активностей
    /// </summary>
    /// <param name="activityTypes"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<ActivityTypeDto> activityTypes)
    {
        await _activityTypeService.Delete(activityTypes);
        return Ok();
    }
}