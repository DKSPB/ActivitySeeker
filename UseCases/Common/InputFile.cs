namespace UseCases.Common;

public class InputFile
{
    public Stream Content { get; }
    public string FileName { get; }

    public InputFile(Stream content, string fileName)
    {
        Content = content ?? throw new ArgumentNullException(nameof(content));
        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
    }
}