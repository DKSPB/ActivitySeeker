using MediatR;

namespace UseCases.ActivityType.Commands.Delete
{
    public record DeleteActivityTypeCommand (Guid ActivityTypeId) : IRequest;
}
