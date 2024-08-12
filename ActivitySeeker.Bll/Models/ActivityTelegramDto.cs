using System.Text;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

public class ActivityTelegramDto: ActivityBaseDto
{
    public ActivityTelegramDto()
    { }
    
    public ActivityTelegramDto(Activity activity) : base(activity)
    { }

    public byte[]? Image { get; set; }

    public bool Selected { get; set; } = false;
}