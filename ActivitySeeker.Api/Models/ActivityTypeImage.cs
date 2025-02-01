using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models
{
    [JsonObject]
    public class ActivityTypeImage
    {
        public Guid ActivityTypeId { get; set; }

        public IFormFile File { get; set; } = default!;
    }
}
