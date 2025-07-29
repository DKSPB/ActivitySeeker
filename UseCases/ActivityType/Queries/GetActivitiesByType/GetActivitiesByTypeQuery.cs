using MediatR;
using UseCases.Common;
using UseCases.Activity.Models;

namespace UseCases.ActivityType.Queries.GetActivitiesByType
{
    public record GetActivitiesByTypeQuery(Guid Id, int Limit = 20, int Offset = 0) : IRequest<PagedResult<ActivityDto>>;
}
