using ActivitySeeker.Bll.Models;
using MediatR;

namespace ActivitySeeker.Bll.ActivityType.Queries.GetById;

public class GetActivityTypeByIdQuery : IRequest<ActivityTypeDto>
{
    public Guid ActivityTypeId { get; set; }
}