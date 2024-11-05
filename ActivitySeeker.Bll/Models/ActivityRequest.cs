namespace ActivitySeeker.Bll.Models;

public class ActivityRequest
{
    public Guid? ActivityTypeId { get; set; }
    
    public DateTime? SearchFrom { get; set; }
    
    public DateTime? SearchTo { get; set; }

    public bool? IsOnline { get; set; }

    public bool? IsPublished { get; set; }
}