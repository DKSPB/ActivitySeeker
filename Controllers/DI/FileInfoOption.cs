namespace Controllers.DI;

public class FileInfoOption
{
    public string RootImageFolder { get; set; } = default!;

    public long MaxFileSize { get; set; }
}