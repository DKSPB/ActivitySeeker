using Newtonsoft.Json;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

[JsonObject]
public class ActivityDto: ActivityBaseDto
{    
    public IEnumerable<ImageDto>? Images { get; set; }
    public ActivityDto()
    { }

    public ActivityDto(Activity activity) : base(activity)
    {
        Id = activity.Id;
        Description = activity.Description;
        StartDate = activity.StartDate;
        ActivityTypeId = activity.ActivityTypeId;
        Link = activity.Link;
        Images = activity.Images?.Select(x => new ImageDto(x));
    }

    public Activity ToActivity()
    {
        return new Activity
        {
            Description = Description,
            StartDate = StartDate,
            ActivityTypeId = ActivityTypeId,
            Images = Images?.Select(x => x.ToEntity()).ToList(),
            Link = Link
        };
    }
}