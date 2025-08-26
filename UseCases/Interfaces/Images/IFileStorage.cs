using UseCases.Activity.Queries.GetImage.Models;

namespace UseCases.Interfaces.Image;

public interface IFileStorage
{
    /// <summary>
    /// Сохраняет файл в сгенерированному пути
    /// </summary>
    /// <param name="fileStream">Поток с данными файла</param>
    /// <param name="filePath">Полный путь к файлу</param>
    /// <param name="fileName">Имя файла</param>
    /// <param name="cancellationToken">Токен прерывания</param>
    Task SaveAsync(Stream fileStream, string filePath, string fileName, CancellationToken cancellationToken = default);

    Task<Stream> GetAsync(string path);

    string GenerateUniqueFileName(string fileExtension);

    string GetImagePath(ImageSize imageSize);
}