using FileSystem.Di;
using Microsoft.Extensions.Options;
using UseCases.Interfaces;

namespace FileSystem.Implementations;

public class LocalFileStorage : IFileStorage
{
    private readonly string _storagePath;
    public LocalFileStorage(IOptions<FileStorageOptions> options)
    {
        _storagePath = Path.Combine(Directory.GetCurrentDirectory(), options.Value.BasePath);
    }

    /// <summary>
    /// Сохраняет файл в сгенерированному пути
    /// </summary>
    /// <param name="contentStream">Поток с данными файла</param>
    /// <param name="extension">Расширение файла</param>
    /// <param name="cancellationToken">Токен прерывания</param>
    /// <returns></returns>
    public async Task<string> SaveAsync(Stream contentStream, string extension, CancellationToken cancellationToken = default)
    {
        CreateDirectoryIfNotExists();
        
        var fileName = GenerateUniqueFileName(extension);

        await using var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
        await contentStream.CopyToAsync(fileStream, cancellationToken);

        return fileName;
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
    private void CreateDirectoryIfNotExists()
    {
        if (!Directory.Exists(_storagePath))
        {
            Directory.CreateDirectory(_storagePath);
        }
    }
    
    /// <summary>
    /// Генерирует уникальное имя файла с расширением
    /// </summary>
    private string GenerateUniqueFileName(string fileExtension)
    {
        var shortName = $"{Guid.NewGuid()}{fileExtension}";
        return Path.Combine(_storagePath, shortName);
    }
}