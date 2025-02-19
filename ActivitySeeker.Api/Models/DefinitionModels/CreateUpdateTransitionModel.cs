using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Api.Models.DefinitionModels;

[JsonObject]
public class CreateUpdateTransitionModel
{
    public int FromStateId { get; set; }
    
    public int ToStateId { get; set; }

    public string Name { get; set; } = default!;
    
    public CreateUpdateTransitionModel()
    { }
    public CreateUpdateTransitionModel(TransitionEntity transitionEntity)
    {
        FromStateId = transitionEntity.FromStateId;
        ToStateId = transitionEntity.ToStateId;
        Name = transitionEntity.Name;
    }
    
    public TransitionEntity ToEntity()
    {
        return new TransitionEntity
        {
            FromStateId = FromStateId,
            ToStateId = ToStateId,
            Name = Name
        };
    }
}