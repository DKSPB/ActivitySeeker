using Newtonsoft.Json;

namespace Controllers.ViewModels;

[JsonObject]
public class RegisterAdmin
{
    public string Username { get; set; } = string.Empty;

    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}