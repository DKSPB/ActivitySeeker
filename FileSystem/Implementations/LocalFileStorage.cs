using FileSystem.Di;
using Microsoft.Extensions.Options;
using UseCases.Activity.Queries.GetImage.Models;
using UseCases.Interfaces.Image;

namespace FileSystem.Implementations;

public class LocalFileStorage : IFileStorage
{
    private readonly FileStorageOptions _storageOptions;
    public LocalFileStorage(IOptions<FileStorageOptions> options)
    {
        _storageOptions = options.Value;
    }
    
    public async Task SaveAsync(Stream contentStream, string filePath, string fileName, CancellationToken cancellationToken = default)
    {
        CreateDirectoryIfNotExists(filePath);

        var fulName = Path.Combine(filePath, fileName);

        await using var fileStream = new FileStream(fulName, FileMode.Create, FileAccess.Write, FileShare.None);
        await contentStream.CopyToAsync(fileStream, cancellationToken);
    }
    
    /// <summary>
    /// Генерирует уникальное имя файла с расширением
    /// </summary>
    public string GenerateUniqueFileName(string fileExtension)
    {
        return $"{Guid.NewGuid()}{fileExtension}";
    }
    
    /// <summary>
    /// Получение изображения по полному имени
    /// </summary>
    /// <param name="path">Полный путь к файлу</param>
    /// <returns></returns>
    public Task<Stream> GetAsync(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"Файл с именем {Path.GetFileName(path)} не существует");

        // OpenRead не грузит файл в память, а открывает поток
        Stream stream = File.OpenRead(path);
        return Task.FromResult<Stream>(stream);
    }

    /// <summary>
    /// Создаёт директорию хранения изображения, если она не существует
    /// </summary>
    private void CreateDirectoryIfNotExists(string storagePath)
    {
        if (!Directory.Exists(storagePath))
        {
            Directory.CreateDirectory(storagePath);
        }
    }

    public string GetImagePath(ImageSize imageSize)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        
        return imageSize switch
        {
            ImageSize.Small => Path.Combine(currentDirectory, _storageOptions.SmallImagePath),
            ImageSize.Medium => Path.Combine(currentDirectory, _storageOptions.MediumImagePath),
            ImageSize.Original => Path.Combine(currentDirectory, _storageOptions.OriginalImagePath),
            _ => Path.Combine(currentDirectory, _storageOptions.OriginalImagePath)
        };
    }
}