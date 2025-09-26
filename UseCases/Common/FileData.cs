namespace UseCases.Common;

public class FileData
{
    public Stream Content { get; set; }
    public string FileExtension { get; set; }
    public string ContentType { get; set; }

    public FileData(Stream content, string fileName, string contentType)
    {
        Content = content;
        FileExtension = fileName;
        ContentType = contentType;
    }
}