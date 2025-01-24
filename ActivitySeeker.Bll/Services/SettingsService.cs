using ActivitySeeker.Bll.Interfaces;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Bll.Services;

public class SettingsService : ISettingsService
{
    public async Task UploadImage(string filePath, Stream file)
    {
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }
    
    public async Task<byte[]?> GetImage(string path)
    {
        var fileInfo = new FileInfo(path);

        if (!fileInfo.Exists)
        {
            return null;
        }
        
        var data = new byte[fileInfo.Length];

        await using var fileStream = fileInfo.OpenRead();
        var readAsync = await fileStream.ReadAsync(data);
        return data;
    }
    public string CombinePathToFile(string webRootPath, string rootImageFolder, string fileName)
    {
        return Path.Combine(webRootPath, rootImageFolder, fileName);
    }
}