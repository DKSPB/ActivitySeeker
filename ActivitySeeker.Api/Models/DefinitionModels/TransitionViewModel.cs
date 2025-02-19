using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models.DefinitionModels;

[JsonObject]
public class TransitionViewModel : CreateUpdateTransitionModel
{
    public int Id { get; set; }
    public TransitionViewModel(TransitionEntity transitionEntity) : base (transitionEntity)
    {
        Id = transitionEntity.Id;
    }
}