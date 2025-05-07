using Controllers.Api.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Controllers.Api.Utils;

public class FilePathGenerator : IFilePathGenerator
{
    private readonly IWebHostEnvironment _environment;
    private readonly ActivitySeekerConfig _botConfig;

    public FilePathGenerator(IWebHostEnvironment environment, IOptions<ActivitySeekerConfig> botConfigOptions)
    {
        _environment = environment;
        _botConfig = botConfigOptions.Value;
    }
    
    public string GeneratePath()
    {
        var filename = Path.GetRandomFileName();
        return Path.Combine(_environment.WebRootPath, _botConfig.RootImageFolder, filename);
    }
}