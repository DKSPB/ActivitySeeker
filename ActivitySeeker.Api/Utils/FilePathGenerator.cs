using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.Utils;

public class FilePathGenerator : IFilePathGenerator
{
    private readonly IWebHostEnvironment _environment;
    private readonly BotConfiguration _botConfig;

    public FilePathGenerator(IWebHostEnvironment env, IOptions<BotConfiguration> botConfigOptions) 
    {
        
    }
    
    public string GeneratePath(string fileName)
    {
        throw new NotImplementedException();
    }
}