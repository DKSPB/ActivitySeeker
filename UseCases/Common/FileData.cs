namespace UseCases.Common;

public class FileData
{
    public Stream Content { get; }
    public string FileExtension { get; }
    public string ContentType { get; }

    public FileData(Stream content, string fileName, string contentType)
    {
        Content = content;
        FileExtension = fileName;
        ContentType = contentType;
    }
}