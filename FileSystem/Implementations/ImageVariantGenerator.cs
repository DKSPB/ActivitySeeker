using FileSystem.DI;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using UseCases.Interfaces;

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
        using var image = await Image.LoadAsync(inputStream);
        
        var small = await ResizeAndConvertToStreamAsync(image, _options.Small, targetExtension);
        var medium = await ResizeAndConvertToStreamAsync(image, _options.Medium, targetExtension);

        return new ImageVariants(small, medium, medium);
    }

    private async Task<Stream> ResizeAndConvertToStreamAsync(Image image, int size, string targetExtension)
    {
        var ms = new MemoryStream();
        
        using var clone = image.Clone(ctx => ctx.Resize(new ResizeOptions
        {
            Mode = ResizeMode.Max,
            Size = new Size(size, size)
        }));
        
        var encoder = GetEncoder(targetExtension);
        await clone.SaveAsync(ms, encoder);
        
        ms.Position = 0;
        return ms;
    }
    
    private IImageEncoder GetEncoder(string extension)
    {
        return extension.ToLower() switch
        {
            "png" => new PngEncoder(),
            _ => new JpegEncoder { Quality = 90 }
        };
    }
}