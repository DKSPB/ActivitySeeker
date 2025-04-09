using ActivitySeeker.UseCases.ActivityType.Dto;
using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models
{
    [JsonObject]
    public class NewActivityType
    {
        public Guid? Id { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public Guid? ParentId { get; set; }

        public ActivityTypeDto ToActivityTypeDto()
        {
            return new ActivityTypeDto
            {
                Id = Id,
                TypeName = TypeName,
                ParentId = ParentId
            };
        }

    }
}
