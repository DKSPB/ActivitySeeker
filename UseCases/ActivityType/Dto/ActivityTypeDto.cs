using Domain.Entities;

namespace ActivitySeeker.UseCases.ActivityType.Dto;

public class ActivityTypeDto
{
    public ActivityTypeDto()
    {
        
    }
    
    public ActivityTypeDto(string typeName)
    {
        TypeName = typeName;
    }
    
    public ActivityTypeDto(Domain.Entities.ActivityType activityType)
    {
        Id = activityType.Id;
        TypeName = activityType.TypeName;
        ParentId = activityType.ParentId;
        ImagePath = activityType.ImagePath;
    }
    
    public Domain.Entities.ActivityType ToActivityType()
    {
        return new Domain.Entities.ActivityType
        {
            TypeName = TypeName,
            ParentId = ParentId
        };
    }

    public Guid? Id { get; set; }
    
    public string TypeName { get; set; } = "Все виды активности";
    
    public Guid? ParentId { get; set; }

    public string? ImagePath { get; set; }
    
    public ActivityTypeDto? Parent { get;  set; }
    
    
    public IEnumerable<ActivityTypeDto>? Children { get; set; }
}