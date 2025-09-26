using MediatR;
using UseCases.ActivityType.Models;

namespace UseCases.ActivityType.Queries.GetById
{
    public record GetActivityTypeByIdQuery(Guid Id) : IRequest<ActivityTypeDto>;
}
