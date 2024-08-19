using System.Text;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Bll.Models;

public class ActivityTelegramDto: ActivityBaseDto
{
    public ActivityTelegramDto()
    { }
    
    public ActivityTelegramDto(Activity activity) : base(activity)
    { }

    [JsonIgnore]
    public IEnumerable<ImageDto>? Images { get; set; }

    public bool Selected { get; set; } = false;
}