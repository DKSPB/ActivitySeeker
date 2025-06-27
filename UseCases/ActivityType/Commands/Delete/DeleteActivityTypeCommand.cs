using MediatR;

namespace UseCases.ActivityType.Commands.Delete
{
    internal record DeleteActivityTypeCommand (List<Guid> ActivityTypeIds) : IRequest;
}
