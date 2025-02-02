using System.Text;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Models;

public class ActivityBaseDto
{
    public ActivityBaseDto()
    {}
    public Guid Id { get; set; }

    public string LinkOrDescription { get; set; } = null!;
    
    public DateTime StartDate { get; set; }
    
    public Guid ActivityTypeId { get; set; }

    public bool? OfferState { get; set; }

    public bool IsOnline { get; set; }

    public int? TgMessageId { get; set; }
    
    public ActivityTypeDto? ActivityType { get; set; }

    public ActivityBaseDto(Activity activity)
    {
        Id = activity.Id;
        ActivityTypeId = activity.ActivityTypeId;
        LinkOrDescription = activity.LinkOrDescription;
        StartDate = activity.StartDate;
        OfferState = activity.IsPublished;
        IsOnline = activity.IsOnline;
        TgMessageId = activity.TgMessageId;
        ActivityType = new ActivityTypeDto(activity.ActivityType);
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