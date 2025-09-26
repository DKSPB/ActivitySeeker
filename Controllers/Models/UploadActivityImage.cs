using Microsoft.AspNetCore.Http;

namespace Controllers.Models;

public class UploadActivityImage
{
    public Guid ActivityId { get; set; }

    public IFormFile File { get; set; } = default!;
}