using ActivitySeeker.Bll.Models;

namespace ActivitySeeker.Api.Models
{
    public class ActivityTypeViewModel
    {
        public Guid? Id { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public ActivityTypeViewModel? Parent { get; set; }

        public IEnumerable<ActivityTypeViewModel>? Children { get; set; }

        public ActivityTypeViewModel(ActivityTypeDto? activityTypeDto)
        {
            Id = activityTypeDto?.Id;
            TypeName = activityTypeDto?.TypeName ?? "";
            Parent = new ActivityTypeViewModel(activityTypeDto?.Parent);
            Children = activityTypeDto?.Children?.Select(x => new ActivityTypeViewModel(x));
        }
    }
}
