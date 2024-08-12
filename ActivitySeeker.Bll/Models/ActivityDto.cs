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
        Description = activity.Description;
        StartDate = activity.StartDate;
        ActivityTypeId = activity.ActivityTypeId;
        Link = activity.Link;
        Image = activity.Image;
    }

    public Activity ToActivity()
    {
        return new Activity
        {
            Description = Description,
            StartDate = StartDate,
            ActivityTypeId = ActivityTypeId,
            Image = Image,
            Link = Link
        };
    }
}