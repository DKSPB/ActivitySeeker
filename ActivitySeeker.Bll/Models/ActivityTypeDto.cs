using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

public class ActivityTypeDto
{

    public ActivityTypeDto()
    {
        
    }
    public ActivityTypeDto(string typeName)
    {
        TypeName = typeName;
    }

    public ActivityTypeDto(ActivityType? activityType)
    {
        Id = activityType?.Id;
        TypeName = activityType?.TypeName ?? "";
        ParentId = activityType?.ParentId;
        Parent = new ActivityTypeDto(activityType?.Parent);
        Children = activityType?.Children?.Select(x => new ActivityTypeDto(x));

    }

    public ActivityType ToActivityType()
    {
        return new ActivityType
        {
            TypeName = TypeName,
            ParentId = ParentId
        };
    }

    public Guid? Id { get; set; }
    public string TypeName { get; set; } = "Все виды активности";
    
    public Guid? ParentId { get; set; }
    
    public ActivityTypeDto? Parent { get;  set; }
    
    public IEnumerable<ActivityTypeDto>? Children { get; set; }
}