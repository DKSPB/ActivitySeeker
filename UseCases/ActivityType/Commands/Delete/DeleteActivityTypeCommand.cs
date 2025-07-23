using MediatR;

namespace UseCases.ActivityType.Commands.Delete
{
    public record DeleteActivityTypeCommand (List<Guid> ActivityTypeIds) : IRequest;
}
