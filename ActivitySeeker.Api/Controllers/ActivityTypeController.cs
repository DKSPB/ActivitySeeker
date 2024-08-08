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
    
    [HttpGet("get")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _activityTypeService.GetTypes());
    }

    [HttpGet("{id:guid}/get")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _activityTypeService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ActivityTypeDto activityType)
    {
        await _activityTypeService.Create(activityType);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ActivityTypeDto activityType)
    {
        await _activityTypeService.Update(activityType);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<ActivityTypeDto> activityTypes)
    {
        await _activityTypeService.Delete(activityTypes);
        return Ok();
    }
}