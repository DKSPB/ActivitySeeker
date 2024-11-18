using System.Text;
using Newtonsoft.Json;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

[JsonObject]
public class ActivityDto: ActivityBaseDto
{   
    public int? CityId { get; set; }
    public byte[]? Image { get; set; }
    public ActivityDto()
    { }

    public ActivityDto(Activity activity) : base(activity)
    {
        Id = activity.Id;
        LinkOrDescription = activity.LinkOrDescription;
        StartDate = activity.StartDate;
        ActivityTypeId = activity.ActivityTypeId;
        IsOnline = activity.IsOnline;
        Image = activity.Image;
        OfferState = activity.IsPublished;
        ActivityType = new ActivityTypeDto(activity.ActivityType);
        CityId = activity.CityId;
    }

    public Activity ToActivity()
    {
        return new Activity
        {
            Id = Id,
            LinkOrDescription = LinkOrDescription,
            StartDate = StartDate,
            ActivityTypeId = ActivityTypeId,
            IsOnline = IsOnline,
            CityId = CityId,
            Image = Image,
            IsPublished = OfferState
        };
    }

    public StringBuilder GetActivityDescription(List<string>? prefixRows = null)
    {
        StringBuilder builder = new();

        prefixRows?.ForEach(x => builder.AppendLine(x));
        builder.AppendLine("Тип активности:");
        builder.AppendLine(ActivityType?.TypeName);
        builder.AppendLine("Формат проведения:");
        builder.AppendLine(IsOnline ? "Онлайн": "Офлайн");
        builder.AppendLine("Дата и время начала:");
        builder.AppendLine(StartDate.ToString("dd.MM.yyyy HH:mm"));
        builder.AppendLine("Описание активности:");
        builder.AppendLine(LinkOrDescription);

        return builder;
    }
}