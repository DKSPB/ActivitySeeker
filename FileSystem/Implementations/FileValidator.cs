using UseCases.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using UseCases.Interfaces;

namespace FileSystem.Implementations
{
    internal class FileValidator : IFileValidator
    {
        private const string DefaultMimetype = "application/octet-stream";

        private static readonly Dictionary<string, byte[]> FileSignatures = new()
        {
            { ".jpg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { ".jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { ".png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } }
        };

        public async Task<FileData> ValidateAndGetStreamAsync(InputFile inputFile, CancellationToken cancellationToken = default)
        {
            if (inputFile.Content == null || inputFile.Content.Length == 0)
            {
                throw new ArgumentException("Файл пустой или произошла ошибка во время загрузки.");
            }
            
            var ext = Path.GetExtension(inputFile.FileName).ToLowerInvariant();
            if (!FileSignatures.ContainsKey(ext))
            {
                throw new InvalidOperationException($"Недопустимое расширение файла: {ext}");
            }
            
            var expectedSignature = FileSignatures[ext];

            // Проверка сигнатуры
            var buffer = new byte[expectedSignature.Length];
            await inputFile.Content.ReadAsync(buffer, cancellationToken);
            if (!buffer.SequenceEqual(expectedSignature))
                throw new InvalidOperationException("Сигнатура файла не соответствует заявленному расширению.");

            // Возвращаемся в начало
            inputFile.Content.Position = 0;

            // Очищаем EXIF и сохраняем в новый поток
            var outputStream = new MemoryStream();
            using (var image = await Image.LoadAsync(inputFile.Content, cancellationToken))
            {
                var encoder = GetEncoder(ext);
                await image.SaveAsync(outputStream, encoder, cancellationToken);
            }

            outputStream.Position = 0;

            var mimeType = ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => DefaultMimetype
            };

            return new FileData(outputStream, ext, mimeType);
        }

        private static IImageEncoder GetEncoder(string ext) =>
            ext switch
            {
                ".jpg" or ".jpeg" => new JpegEncoder(),
                ".png" => new PngEncoder(),
                _ => throw new InvalidOperationException("Нет подходящего энкодера.")
            };
    }
}