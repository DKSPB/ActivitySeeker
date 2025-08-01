using Microsoft.AspNetCore.Http;
using UseCases.Common;

namespace Controllers.Utils;

public static class FormFileExtensions
{
    public static FileData Convert(this IFormFile file)
    {
        var stream = file.OpenReadStream();
        return new FileData(stream, Path.GetExtension(file.FileName), file.ContentType);
    }
}