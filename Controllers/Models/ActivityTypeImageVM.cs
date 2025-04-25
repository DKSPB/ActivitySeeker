using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Controllers.Models
{
    [JsonObject]
    public class ActivityTypeImageVM
    {
        public Guid ActivityTypeId { get; set; }

        public IFormFile File { get; set; } = default!;
    }
}
