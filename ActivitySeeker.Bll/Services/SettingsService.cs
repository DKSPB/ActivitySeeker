using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Infrastructure.Models.Settings;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Bll.Services;

public class SettingsService : ISettingsService
{
    public async Task UploadImage(string filePath, Stream file)
    {
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
    }
}