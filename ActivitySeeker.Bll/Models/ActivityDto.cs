using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Bll.Models;

[JsonObject]
public class ActivityDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;
    
    public DateTime StartDate { get; set; }
    
    public Guid ActivityTypeId { get; set; }
    
    public ActivityDto()
    { }
    public ActivityDto(Activity activity)
    {
        Id = activity.Id;
        Name = activity.Name;
        Description = activity.Description;
        StartDate = activity.StartDate;
        ActivityTypeId = activity.ActivityTypeId;
    }

    public static Activity ToActivity(ActivityDto activityDto)
    {
        return new Activity
        {
            Id = activityDto.Id,
            Name = activityDto.Name,
            Description = activityDto.Description,
            StartDate = activityDto.StartDate,
            ActivityTypeId = activityDto.ActivityTypeId
        };
    }

    public bool Selected { get; set; }
}