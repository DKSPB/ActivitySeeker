using MediatR;

namespace UseCases.ActivityType.Commands.Create;

public record CreateActivityTypeCommand : IRequest
{
    public string TypeName { get; init; } = string.Empty;
    
    public Guid? ParentId { get; init; }
}