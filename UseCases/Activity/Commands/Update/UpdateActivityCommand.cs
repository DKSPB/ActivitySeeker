using MediatR;
using UseCases.Activity.Commands.Common;

namespace UseCases.Activity.Commands.Update;

public class UpdateActivityCommand : ActivityCommandBase, IRequest
{
    public Guid Id { get; init; }
}