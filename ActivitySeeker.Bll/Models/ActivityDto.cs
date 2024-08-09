using System.Text;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace ActivitySeeker.Bll.Models;

[JsonObject]
public class ActivityDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;
    
    public DateTime StartDate { get; set; }
    
    public Guid ActivityTypeId { get; set; }
    
    public string? Link { get; set; }
     
    public byte[]? Image { get; set; }
    
    public bool Selected { get; set; }
    
    public ActivityDto()
    { }
    public ActivityDto(Activity activity)
    {
        Id = activity.Id;
        Name = activity.Name;
        Description = activity.Description;
        StartDate = activity.StartDate;
        ActivityTypeId = activity.ActivityTypeId;
        Link = activity.Link;
        Image = activity.Image;
    }

    public static Activity ToActivity(ActivityDto activityDto)
    {
        return new Activity
        {
            Name = activityDto.Name,
            Description = activityDto.Description,
            StartDate = activityDto.StartDate,
            ActivityTypeId = activityDto.ActivityTypeId,
            Image = activityDto.Image,
            Link = activityDto.Link
        };
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.Append(Name);
        stringBuilder.Append('\n');
        stringBuilder.Append(Description);
        return stringBuilder.ToString();
    }
}