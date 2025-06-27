using Newtonsoft.Json;

namespace Controllers.ViewModels
{
    [JsonObject]
    public class ActivityTypeViewModel
    {
        public Guid? Id { get; set; }

        public bool IsOnline { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }
        
        public string? ParentTypeName { get; set; }

        /*public ActivityTypeViewModel(ActivityTypeDto? activityTypeDto)
        {
            Id = activityTypeDto?.Id;
            TypeName = activityTypeDto?.TypeName ?? "";
            ParentId = activityTypeDto?.ParentId ?? null;
            ParentTypeName = activityTypeDto?.Parent?.TypeName;
        }*/
    }
}
