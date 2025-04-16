namespace ActivitySeeker.UseCases.ActivityType.Dto;

public class UploadActivityTypeImageDto
{
    public Guid ActivityTypeId { get; set; }
    
    public string Path { get; set; } = string.Empty;

    public Stream Image { get; set; } = default!;
}