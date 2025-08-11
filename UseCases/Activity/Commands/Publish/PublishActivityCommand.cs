using MediatR;

namespace UseCases.Activity.Commands.Publish
{
    public record PublishActivityCommand(Guid ActivityId) : IRequest;
}
