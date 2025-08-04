using UseCases.Common;
using SixLabors.ImageSharp;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp.Formats;
using Microsoft.AspNetCore.StaticFiles;

namespace Controllers.Utils
{
    internal static class FileValidator
    {
        private const string defaultMimetype = "application/octet-stream";

        private static readonly Dictionary<string, byte[]> _fileSignatures = new()
        {
            { ".jpg",  new byte[] { 0xFF, 0xD8, 0xFF } },
            { ".jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { ".png",  new byte[] { 0x89, 0x50, 0x4E, 0x47 } }
        };

        public static async Task<FileData> ValidateAndGetStreamAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Файл пустой или произошла ошибка во время загрузки.");
            }    
                
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!_fileSignatures.ContainsKey(ext))
            {
                throw new InvalidOperationException($"Недопустимое расширение файла: {ext}");
            }
                
            var expectedSignature = _fileSignatures[ext];

            using (var sigStream = file.OpenReadStream())
            {
                var buffer = new byte[expectedSignature.Length];
                await sigStream.ReadAsync(buffer, cancellationToken);

                if (!buffer.SequenceEqual(expectedSignature))
                {
                    throw new InvalidOperationException("Сигнатура файла не соответствует заявленному расширению.");
                }    
                    
            }

            // Очищаем EXIF
            var outputStream = new MemoryStream();
            using (var image = await Image.LoadAsync(file.OpenReadStream(), cancellationToken))
            {
                // Сохраняем без метаданных
                var encoder = GetEncoder(ext);
                await image.SaveAsync(outputStream, encoder, cancellationToken);
            }
            outputStream.Position = 0;

            var mimeType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => defaultMimetype
            };

            return new FileData (outputStream, mimeType, file.Name);
        }

        private static IImageEncoder GetEncoder(string ext) =>
            ext switch
            {
                ".jpg" or ".jpeg" => new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder(),
                ".png" => new SixLabors.ImageSharp.Formats.Png.PngEncoder(),
                _ => throw new InvalidOperationException("Нет подходящего энкодера.")
            };

        /// <summary>
        /// Получение Mime-типа по расширению файла
        /// </summary>
        /// <param name="fileExtansion">Расширение файла</param>
        /// <returns>Mime-тип</returns>
        public static string GetMimeTypeByExtension(string fileExtansion)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileExtansion, out var contentType))
            {
                contentType = defaultMimetype;
            }

            return contentType;
        }
    }
}
