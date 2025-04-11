namespace ActivitySeeker.UseCases.ActivityType.Dto;

public class CreateActivityTypeDto
{
    public string TypeName { get; set; } = string.Empty;
    
    public Guid? ParentId { get; set; }
}