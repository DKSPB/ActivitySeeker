namespace FileSystem.Di;

public class FileStorageOptions
{
    public const string SectionName = "FileStorage";
    public string BasePath { get; set; } = string.Empty;
}