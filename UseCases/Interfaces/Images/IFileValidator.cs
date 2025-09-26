using UseCases.Common;

namespace UseCases.Interfaces.Image;

public interface IFileValidator
{
    Task<FileData> ValidateAndGetStreamAsync(InputFile inputFile, CancellationToken cancellationToken = default);
}