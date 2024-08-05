using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

public class ActivityTypeDto
{
    public ActivityTypeDto()
    { }

    public ActivityTypeDto(ActivityType activityType)
    {
        Id = activityType.Id;
        TypeName = activityType.TypeName;
    }

    public ActivityType ToActivityType()
    {
        return new ActivityType
        {
            Id = Id,
            TypeName = TypeName
        };
    }

    public Guid Id { get; set; }
    public string TypeName { get; set; } = default!;
}