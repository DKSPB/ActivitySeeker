using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Controllers.ViewModels;

[JsonObject]
public class CityImage
{
    public int CityId { get; set; }
    
    public IFormFile File { get; set; } = default!;
}