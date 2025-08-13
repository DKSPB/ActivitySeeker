using MediatR;

namespace UseCases.Activity.Commands.Delete;

public record DeleteActivityCommand(Guid ActivityId) : IRequest;