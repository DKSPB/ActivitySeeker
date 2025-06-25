using MediatR;

namespace UseCases.ActivityType.Commands.Create;

public record CreateActivityTypeCommand : IRequest
{
    public string TypeName { get; init; }
    
    public Guid? ParentId { get; init; }
}