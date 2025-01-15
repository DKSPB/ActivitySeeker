namespace ActivitySeeker.Bll.Interfaces;

public interface ISettingsService
{
    Task UploadImage(string filePath, Stream file);
}