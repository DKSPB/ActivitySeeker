namespace UseCases.Interfaces.Image;

public interface IImageVariantGenerator
{
    /// <summary>
    /// Генерирует три версии изображения (Small, Medium, Large) с заданным расширением.
    /// </summary>
    /// <param name="inputStream">Исходный поток изображения</param>
    /// <param name="targetExtension">Расширение (jpg, png и т.д.)</param>
    /// <returns>Результат с тремя вариантами изображения</returns>
    Task<ImageVariants> GenerateAsync(Stream inputStream, string targetExtension);
    
}

public record ImageVariants(
    Stream Original,
    Stream Medium,
    Stream Small
);