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
            TypeName = TypeName
        };
    }

    public Guid? Id { get; set; }
    public string TypeName { get; set; } = "Все виды активности";
}