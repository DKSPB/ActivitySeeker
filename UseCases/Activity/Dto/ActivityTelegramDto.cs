using System.Text;
using ActivitySeeker.UseCases.Models;
using Domain.Entities;

namespace ActivitySeeker.UseCases.Models;

public class ActivityTelegramDto: ActivityBaseDto
{
    public ActivityTelegramDto()
    { }
    
    public ActivityTelegramDto(Activity activity) : base(activity)
    { }

    public byte[]? Image { get; set; }

    public bool Selected { get; set; }
}