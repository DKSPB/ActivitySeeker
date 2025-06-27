using MediatR;
using UseCases.ActivityType.Models;

namespace UseCases.ActivityType.Queries.GetById
{
    internal record GetActivityTypeByIdQuery(Guid Id) : IRequest<ActivityTypeDto>;
}
