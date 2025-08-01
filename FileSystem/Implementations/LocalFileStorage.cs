using FileSystem.Di;
using Microsoft.Extensions.Options;
using UseCases.Interfaces;

namespace FileSystem.Implementations;

public class LocalFileStorage : IFileStorage
{
    public LocalFileStorage(IOptions<FileStorageOptions> options)
    {
        var storagePath = Path.Combine(Directory.GetCurrentDirectory(), options.Value.BasePath);

        if (!Directory.Exists(storagePath))
        {
            Directory.CreateDirectory(storagePath);
        }
    }
    
    /// <summary>
    /// Генерирует уникальное имя файла с расширением
    /// </summary>
    public string GenerateUniqueFileName(string originalFileName)
    {
        var extension = Path.GetExtension(originalFileName);
        var uniqueName = $"{Guid.NewGuid()}{extension}";
        return uniqueName;
    }

    public Task<string> SaveAsync(Stream fileStream, string extension, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}