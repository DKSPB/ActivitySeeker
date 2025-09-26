namespace FileSystem.Di;

public class FileStorageOptions
{
    public const string SectionName = "FileStorage";
    public string SmallImagePath { get; set; } = string.Empty;
    public string MediumImagePath { get; set; } = string.Empty;
    public string OriginalImagePath { get; set; } = string.Empty;
}