using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models.DefinitionModels
{
    [JsonObject]
    public class StateViewModel: CreateUpdateStateModel
    {
        public int Id { get; set; }

        public List<BotTransition>? Transitions { get; set; }

    }
}
