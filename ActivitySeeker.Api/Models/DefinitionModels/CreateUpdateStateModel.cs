using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models.DefinitionModels
{
    [JsonObject]
    public class CreateUpdateStateModel
    {
        public string Name { get; set; } = default!;
    }
}
