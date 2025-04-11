using ActivitySeeker.UseCases.ActivityType.Dto;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Queries.GetById;

public class GetActivityTypeByIdQuery : IRequest<ActivityTypeDto>
{
    public GetActivityTypeByIdQuery(Guid activityTypeId)
    {
        ActivityTypeId = activityTypeId;
    }
    public Guid ActivityTypeId { get; }
}