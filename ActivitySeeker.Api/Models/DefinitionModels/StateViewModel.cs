using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models.DefinitionModels
{
    [JsonObject]
    public class StateViewModel : CreateUpdateStateModel
    {
        public int Id { get; set; }

        public IEnumerable<TransitionViewModel>? IncomingTransitions { get; set; }
        
        public IEnumerable<TransitionViewModel>? OutgoingTransitions { get; set; }

        public StateViewModel(StateEntity stateEntityEntity) : base (stateEntityEntity)
        {
            Id = stateEntityEntity.Id;
            Name = stateEntityEntity.Name;
            IncomingTransitions = stateEntityEntity.IncomingTransitions.Select(x => new TransitionViewModel(x));
            OutgoingTransitions = stateEntityEntity.OutgoingTransitions.Select(x => new TransitionViewModel(x));
        }
    }
}
