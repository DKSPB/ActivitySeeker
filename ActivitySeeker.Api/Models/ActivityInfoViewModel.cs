using ActivitySeeker.Bll.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ActivitySeeker.Api.Models;

public class ActivityInfoViewModel
{
    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; set; }
    
    public Guid ActivityTypeId { get; set; }

    public string? Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public string? Link { get; set; }
    
    public List<Guid>? Images { get; set; }

    public ActivityInfoViewModel(ActivityDto activityDto)
    {
        Id = activityDto.Id;
        ActivityTypeId = activityDto.ActivityTypeId;
        Description = activityDto.Description;
        StartDate = activityDto.StartDate;
        Link = activityDto.Link;
        Images = activityDto.Images?.Select(x => x.Id).ToList();
    }
}