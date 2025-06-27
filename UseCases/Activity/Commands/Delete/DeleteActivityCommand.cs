using MediatR;

namespace UseCases.Activity.Commands.Delete;

public record DeleteActivityCommand(List<Guid> ActivityIds) : IRequest;