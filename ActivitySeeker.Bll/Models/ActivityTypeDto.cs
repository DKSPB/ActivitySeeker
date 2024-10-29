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

    public ActivityTypeDto(ActivityType activityType)
    {
        Id = activityType.Id;
        TypeName = activityType.TypeName;
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
    
    public ActivityType? Parent { get; set; }
    
    public IEnumerable<ActivityType>? Children { get; set; }
}