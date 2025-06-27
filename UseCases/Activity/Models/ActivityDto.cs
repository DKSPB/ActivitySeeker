namespace UseCases.Activity.Models;

public class ActivityDto
{   
    public Guid Id { get; set; }
    
    public long VkAuthorId { get; set; }

    public string LinkOrDescription { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public Guid ActivityTypeId { get; set; }

    public bool IsOnline { get; set; }
    
    public bool IsPublished { get; set; }
    
    public int? CityId { get; set; }
    
    public byte[]? Image { get; set; }
}