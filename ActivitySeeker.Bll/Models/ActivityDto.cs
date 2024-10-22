using Newtonsoft.Json;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

[JsonObject]
public class ActivityDto: ActivityBaseDto
{    
    public byte[]? Image { get; set; }
    public ActivityDto()
    { }

    public ActivityDto(Activity activity) : base(activity)
    {
        Id = activity.Id;
        LinkOrDescription = activity.LinkOrDescription;
        StartDate = activity.StartDate;
        ActivityTypeId = activity.ActivityTypeId;
        Image = activity.Image;
        OfferState = activity.IsPublished;
    }

    public Activity ToActivity()
    {
        return new Activity
        {
            Id = Id,
            LinkOrDescription = LinkOrDescription,
            StartDate = StartDate,
            ActivityTypeId = ActivityTypeId,
            Image = Image,
            IsPublished = OfferState
        };
    }
}