using FileSystem.DI;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using UseCases.Interfaces.Image;

namespace FileSystem.Implementations;

public class ImageVariantGenerator : IImageVariantGenerator
{
    private readonly ImageVariantOptions _options;

    public ImageVariantGenerator(IOptions<ImageVariantOptions> options)
    {
        _options = options.Value;
    }

    public async Task<ImageVariants> GenerateAsync(Stream inputStream, string targetExtension)
    {
        if (inputStream == null || inputStream.Length == 0)
            throw new ArgumentException("Пустой поток изображения", nameof(inputStream));

        var ext = NormalizeExtension(targetExtension);

        // Загружаем оригинал в ImageSharp для ресайза маленькой и средней версий
        using var image = await Image.LoadAsync(inputStream);

        var small  = await ResizeAndConvertToStreamAsync(image, _options.Small, ext);
        var medium = await ResizeAndConvertToStreamAsync(image, _options.Medium, ext);

        // Оригинал — просто копируем входной поток в новый MemoryStream
        inputStream.Position = 0; // возвращаемся в начало
        var original = new MemoryStream();
        await inputStream.CopyToAsync(original);
        original.Position = 0;

        return new ImageVariants(original, medium, small);
    }

    private static async Task<Stream> ResizeAndConvertToStreamAsync(Image image, int size, string extension)
    {
        var ms = new MemoryStream();

        using (var clone = image.Clone(ctx => ctx.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Max,
            Size = new Size(size, size)
        })))
        {
            var encoder = GetEncoder(extension);
            await clone.SaveAsync(ms, encoder);
        }

        ms.Position = 0;
        return ms;
    }

    private static string NormalizeExtension(string ext) =>
        ext.TrimStart('.').ToLowerInvariant();

    private static IImageEncoder GetEncoder(string extension) =>
        extension switch
        {
            "png"  => new PngEncoder(),
            "jpg"  => new JpegEncoder { Quality = 90 },
            "jpeg" => new JpegEncoder { Quality = 90 },
            _      => throw new NotSupportedException($"Неподдерживаемое расширение: {extension}")
        };
}