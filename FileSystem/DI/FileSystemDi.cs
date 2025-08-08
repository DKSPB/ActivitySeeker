using FileSystem.Di;
using FileSystem.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UseCases.Interfaces;


namespace FileSystem.DI;

public static class FileSystemDi
{
    public static IServiceCollection AddFileSystemInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<FileStorageOptions>(config.GetSection(FileStorageOptions.SectionName));
        services.Configure<ImageVariantOptions>(config.GetSection(ImageVariantOptions.SectionName));

        services.AddScoped<IFileValidator, FileValidator>();
        services.AddScoped<IImageVariantGenerator, ImageVariantGenerator>();
        services.AddScoped<IFileStorage, LocalFileStorage>();

        return services;
    }
}