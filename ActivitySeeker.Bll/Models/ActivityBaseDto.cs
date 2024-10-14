using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

public class ActivityBaseDto
{
    public ActivityBaseDto()
    {
        
    }
    public Guid Id { get; set; }

    public string? Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public Guid ActivityTypeId { get; set; }
    
    public string? Link { get; set; }

    public bool OfferState { get; set; }

    public ActivityBaseDto(Activity activity)
    {
        Id = activity.Id;
        ActivityTypeId = activity.ActivityTypeId;
        Description = activity.Description;
        StartDate = activity.StartDate;
        Link = activity.Link;
        OfferState = activity.OfferState;
    }
}