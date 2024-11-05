using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

public class ActivityBaseDto
{
    public ActivityBaseDto()
    {}
    public Guid Id { get; set; }

    public string LinkOrDescription { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    
    public Guid ActivityTypeId { get; set; }

    public bool OfferState { get; set; }

    public bool IsOnline { get; set; }
    
    public ActivityTypeDto? ActivityType { get; set; }

    public ActivityBaseDto(Activity activity)
    {
        Id = activity.Id;
        ActivityTypeId = activity.ActivityTypeId;
        LinkOrDescription = activity.LinkOrDescription;
        StartDate = activity.StartDate;
        OfferState = activity.IsPublished;
        IsOnline = activity.IsOnline;
        ActivityType = new ActivityTypeDto(activity.ActivityType);
    }
}