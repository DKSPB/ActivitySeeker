using ActivitySeeker.UseCases.ActivityType.Dto;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Queries.GetAll;

public class GetAllActivityTypeQuery : IRequest<List<ActivityTypeDto>>
{
    
}