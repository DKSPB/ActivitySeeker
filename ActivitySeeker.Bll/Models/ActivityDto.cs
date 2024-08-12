using Newtonsoft.Json;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

[JsonObject]
public class ActivityDto: ActivityBaseDto
{    
 
    public ActivityDto():base()
    { }

    public ActivityDto(Activity activity) : base(activity)
    {
        Id = activity.Id;
        Name = activity.Name;
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
            Name = Name,
            Description = Description,
            StartDate = StartDate,
            ActivityTypeId = ActivityTypeId,
            Image = Image,
            Link = Link
        };
    }

    public byte[]? Image { get; set; }
}