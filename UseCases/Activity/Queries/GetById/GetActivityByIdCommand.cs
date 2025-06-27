using MediatR;
using UseCases.Activity.Models;

namespace UseCases.Activity.Queries.GetById
{
    internal record GetActivityByIdCommand(Guid Id) : IRequest<ActivityDto>;
}
