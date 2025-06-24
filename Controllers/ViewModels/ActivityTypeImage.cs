using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Controllers.ViewModels
{
    [JsonObject]
    public class ActivityTypeImage
    {
        public Guid ActivityTypeId { get; set; }

        public IFormFile File { get; set; } = default!;
    }
}
