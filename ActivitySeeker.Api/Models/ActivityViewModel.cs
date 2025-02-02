using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models
{
    [JsonObject]
    public class ActivityViewModel
    {
        public Guid Id { get; set; }

        public string LinkOrDescription { get; set; }

        public DateTime StartDate { get; set; }

        public Guid ActivityTypeId { get; set; }

        public string TypeName { get; set; }

        public bool? IsPublished { get; set; }

        public bool IsOnline { get; set; }

        public int? CityId { get; set; }

        public string? CityName { get; set; }

        public int? TgMessageId { get; set; }

        public ActivityViewModel(Activity activity)
        {
            Id = activity.Id;
            LinkOrDescription = activity.LinkOrDescription;
            StartDate = activity.StartDate;
            ActivityTypeId = activity.ActivityType.Id;
            TypeName = activity.ActivityType.TypeName;
            IsPublished = activity.IsPublished;
            IsOnline = activity.IsOnline;
            CityId = activity.ActivityCity?.Id;
            CityName = activity.ActivityCity?.Name;
            TgMessageId = activity.TgMessageId;
        }
    }
}
