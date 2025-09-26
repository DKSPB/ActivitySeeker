namespace ActivitySeeker.Api.Extensions.Cors;

public class CorsSettings
{
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    public string[] AllowedPatternOrigins { get; set; } = Array.Empty<string>();
}