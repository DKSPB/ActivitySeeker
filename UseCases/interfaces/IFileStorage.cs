namespace UseCases.Interfaces;

public interface IFileStorage
{
    Task<string> SaveAsync(Stream fileStream, string extension, CancellationToken cancellationToken = default);

    Task<Stream> GetAsync(string path);
}