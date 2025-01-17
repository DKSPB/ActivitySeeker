using ActivitySeeker.Infrastructure.Models.Settings;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Infrastructure;

public class FileProvider
{
    private readonly Settings _settings;
    private readonly string _rootImageFolder;

    public FileProvider(IOptions<Settings> optionSettings)
    {
        _settings = optionSettings.Value;
        _rootImageFolder = _settings.RootImageFolder;
    }
    
    public async Task UploadImage(string filePath, Stream file)
    {
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }

    public byte[] GetTgMainMenuImage()
    {
        var tgMainMenuImageName = _settings.TelegramBotSettings;
    }
    
    public async Task<byte[]> GetImage(string path)
    {
        var fileInfo = new FileInfo(path);
        
        var data = new byte[fileInfo.Length];

        await using var fileStream = fileInfo.OpenRead();
        var readAsync = await fileStream.ReadAsync(data);
        return data;
    }
}