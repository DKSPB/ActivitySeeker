using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models;

[JsonObject]
public class CityImage
{
    public int CityId { get; set; }
    
    public IFormFile File { get; set; } = default!;
}