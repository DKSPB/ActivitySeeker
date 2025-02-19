using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models.DefinitionModels
{
    [JsonObject]
    public class CreateUpdateStateModel
    {
        public string Name { get; set; } = default!;

        public CreateUpdateStateModel()
        { }
        public CreateUpdateStateModel(StateEntity stateEntity)
        {
            Name = stateEntity.Name;
        }
        
        public StateEntity ToEntity()
        {
            return new StateEntity
            {
                Name = Name
            };
        }
    }
}
