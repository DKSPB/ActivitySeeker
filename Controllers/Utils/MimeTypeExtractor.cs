using Microsoft.AspNetCore.StaticFiles;

namespace Controllers.Utils;

public static class MimeTypeExtractor
{
    private const string DefaultMimetype = "application/octet-stream";
    
    /// <summary>
    /// Получение Mime-типа по расширению файла
    /// </summary>
    /// <param name="fileExtension">Расширение файла</param>
    /// <returns>Mime-тип</returns>
    public static string GetMimeTypeByExtension(string fileExtension)
    {
        var provider = new FileExtensionContentTypeProvider();

        if (!provider.TryGetContentType(fileExtension, out var contentType))
        {
            contentType = DefaultMimetype;
        }

        return contentType;
    }
}