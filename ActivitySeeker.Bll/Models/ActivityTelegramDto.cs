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
    
    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.Append(Name);
        stringBuilder.Append('\n');
        stringBuilder.Append(Description);
        return stringBuilder.ToString();
    }
}