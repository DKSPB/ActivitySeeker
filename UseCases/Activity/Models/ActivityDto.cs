namespace UseCases.Activity.Models;

public class ActivityDto
{   
    public Guid Id { get; set; }

    public string ActivityType { get; set; } = default!;
    
    public long UserId { get; set; }

    public string Description { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public int TimeZone { get; set; }

    public bool IsOnline { get; set; }
    
    public bool IsPublished { get; set; }
    
    public int? CityId { get; set; }
}