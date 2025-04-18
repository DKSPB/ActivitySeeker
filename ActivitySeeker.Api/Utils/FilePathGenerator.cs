using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.Utils;

public class FilePathGenerator : IFilePathGenerator
{
    private readonly IWebHostEnvironment _environment;
    private readonly BotConfiguration _botConfig;

    public FilePathGenerator(IWebHostEnvironment environment, IOptions<BotConfiguration> botConfigOptions)
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