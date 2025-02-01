using ActivitySeeker.Bll.Models;
using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models
{
    [JsonObject]
    public class ActivityTypeViewModel
    {
        public Guid? Id { get; set; }

        public bool IsOnline { get; set; }

        public string TypeName { get; set; }

        public Guid? ParentId { get; set; }
        
        public string? ParentTypeName { get; set; }

        public ActivityTypeViewModel(ActivityTypeDto? activityTypeDto)
        {
            Id = activityTypeDto?.Id;
            TypeName = activityTypeDto?.TypeName ?? "";
            ParentId = activityTypeDto?.ParentId ?? null;
            ParentTypeName = activityTypeDto?.Parent?.TypeName;
        }
    }
}
