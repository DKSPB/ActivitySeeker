namespace ActivitySeeker.UseCases.ActivityType.Dto;

public class ActivityTypeViewModel
{
    public Guid? Id { get; set; }

    public string TypeName { get; set; } = string.Empty;

    public Guid? ParentId { get; set; }
        
    public string? ParentTypeName { get; set; }
}