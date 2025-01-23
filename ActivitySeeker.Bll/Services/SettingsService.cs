using ActivitySeeker.Bll.Interfaces;
using Microsoft.Extensions.Options;
using ActivitySeeker.Infrastructure.Models.Settings;

namespace ActivitySeeker.Bll.Services;

public class SettingsService : ISettingsService
{
    private readonly Settings _settings;
    public SettingsService(IOptions<Settings> _settingsOtions)
    {
        _settings = _settingsOtions.Value;
    }

    /// <summary>
    /// Загрузка файла по заданному пути
    /// </summary>
    /// <param name="filePath">Полный путь к файлу</param>
    /// <param name="file"> Содержимое файла (в потоке)</param>
    /// <returns></returns>
    public async Task UploadImage(string filePath, Stream file)
    {
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }

    /// <summary>
    /// Получение файла в формате массив байтов
    /// </summary>
    /// <param name="path">Полный путь к файлу</param>
    /// <returns></returns>
    public async Task<byte[]> GetImage(string path)
    {
        var fileInfo = new FileInfo(path);

        var data = new byte[fileInfo.Length];

        await using var fileStream = fileInfo.OpenRead();
        var readAsync = await fileStream.ReadAsync(data);
        return data;
    }

    /// <summary>
    /// Конфигурирует путь к изображениям из настроек конфигурации и переменной среды окружения webRootPath
    /// </summary>
    /// <param name="webRootPath">Переменная среды окружения, хронящая в себе путь к проекту</param>
    /// <returns></returns>
    public string CombinePathToFile(string webRootPath, string fileName)
    {
        var filesFoler = _settings.RootImageFolder;
        return Path.Combine(webRootPath, filesFoler, fileName);
    }
}