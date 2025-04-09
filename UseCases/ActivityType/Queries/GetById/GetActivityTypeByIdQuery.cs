using ActivitySeeker.UseCases.ActivityType.Dto;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Queries.GetById;

public class GetActivityTypeByIdQuery : IRequest<ActivityTypeDto>
{
    public Guid ActivityTypeId { get; set; }
}