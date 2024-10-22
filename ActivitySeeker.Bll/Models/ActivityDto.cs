using System.Text;
using Newtonsoft.Json;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

[JsonObject]
public class ActivityDto: ActivityBaseDto
{    
    public byte[]? Image { get; set; }
    public ActivityDto()
    { }

    public ActivityDto(Activity activity) : base(activity)
    {
        Id = activity.Id;
        LinkOrDescription = activity.LinkOrDescription;
        StartDate = activity.StartDate;
        ActivityTypeId = activity.ActivityTypeId;
        Image = activity.Image;
        OfferState = activity.IsPublished;
        ActivityType = new ActivityTypeDto(activity.ActivityType);
    }

    public Activity ToActivity()
    {
        return new Activity
        {
            Id = Id,
            LinkOrDescription = LinkOrDescription,
            StartDate = StartDate,
            ActivityTypeId = ActivityTypeId,
            Image = Image,
            IsPublished = OfferState
        };
    }

    public StringBuilder GetActivityDescription(List<string>? prefixRows = null)
    {
        StringBuilder builder = new();

        prefixRows?.ForEach(x => builder.AppendLine(x));
        //builder.AppendLine("Эта активность будет предложена для публикации.");
        //builder.AppendLine("Убедись, что данные заполнены корректно ");
        builder.AppendLine("Тип активности:");
        builder.AppendLine(ActivityType?.TypeName);
        builder.AppendLine("Дата и время начала:");
        builder.AppendLine(StartDate.ToString("dd.MM.yyyy HH:mm"));
        builder.AppendLine("Описание активности:");
        builder.AppendLine(LinkOrDescription);

        return builder;
    }
}