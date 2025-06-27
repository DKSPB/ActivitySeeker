using MediatR;
using UseCases.ActivityType.Models;

namespace UseCases.ActivityType.Queries.GetById
{
    internal record GetActivityTypeByIdCommand(Guid Id) : IRequest<ActivityTypeDto>;
}
