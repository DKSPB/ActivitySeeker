using UseCases.Common;

namespace UseCases.Interfaces;

public interface IFileValidator
{
    Task<FileData> ValidateAndGetStreamAsync(InputFile inputFile, CancellationToken cancellationToken = default);
}