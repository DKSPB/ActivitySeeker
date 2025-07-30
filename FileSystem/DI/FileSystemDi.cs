using FileSystem.Di;
using FileSystem.Implementations;
using FileSystem.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FileSystem.DI;

public static class FileSystemDi
{
    public static IServiceCollection AddFileSystemInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<FileStorageOptions>(config.GetSection(FileStorageOptions.SectionName));

        services.AddScoped<IFileStorage, LocalFileStorage>();

        return services;
    }
}