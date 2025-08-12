using MediatR;
using UseCases.ActivityType.Models;
using UseCases.Common;

namespace UseCases.ActivityType.Queries.GetAll
{
    public record GetActivityTypesQuery(int Limit = 20, int Offset = 1) : IRequest<PagedResult<ActivityTypeDto>>;
}
