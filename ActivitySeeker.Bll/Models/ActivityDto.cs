using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

public class ActivityDto
{
    public ActivityDto(Activity activity)
    {
        Activity = activity;
    }
    public Activity Activity { get; set; }

    public bool Selected { get; set; } = false;
}