using MediatR;
using UseCases.Activity.Commands.Common;

namespace UseCases.Activity.Commands.Create;

public class CreateActivityCommand : ActivityCommandBase, IRequest
{
    public long UserId { get; init; }
}