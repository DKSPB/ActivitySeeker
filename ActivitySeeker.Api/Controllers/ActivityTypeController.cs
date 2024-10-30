using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/activityType")]
public class ActivityTypeController : ControllerBase
{
    private readonly ILogger<ActivityController> _logger;
    private readonly IActivityTypeService _activityTypeService;
    
    public ActivityTypeController(ILogger<ActivityController> logger, IActivityTypeService activityTypeService)
    {
        _logger = logger;
        _activityTypeService = activityTypeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var activityTypes = await _activityTypeService.GetAll();

        foreach (var activityType in activityTypes)
        {
            activityType.Parent = 
                activityType.ParentId is null ? null : await _activityTypeService.GetById(activityType.ParentId.Value);
        }
        
        return Ok(activityTypes.Select(x => new ActivityTypeViewModel(x)));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return Ok(await _activityTypeService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] NewActivityType activityType)
    {
        await _activityTypeService.Create(activityType.ToActivityTypeDto());
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] NewActivityType activityType)
    {
        await _activityTypeService.Update(activityType.ToActivityTypeDto());
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] List<Guid> activityTypeIds)
    {
        await _activityTypeService.Delete(activityTypeIds);
        return Ok();
    }
}