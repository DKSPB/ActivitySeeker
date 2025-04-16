using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models
{
    [JsonObject]
    public class ActivityTypeImageVM
    {
        public Guid ActivityTypeId { get; set; }

        public IFormFile File { get; set; } = default!;
    }
}
