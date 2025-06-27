using MediatR;
using UseCases.ActivityType.Models;

namespace UseCases.ActivityType.Queries.GetAll
{
    public record GetActivityTypesCommand() : IRequest<List<ActivityTypeDto>>;
}
