using MediatR;
using UseCases.ActivityType.Models;

namespace UseCases.ActivityType.Commands.Create;

public record CreateActivityTypeCommand : IRequest<ActivityTypeDto>
{
    public string TypeName { get; init; } = string.Empty;
    
    public Guid? ParentId { get; init; }
}