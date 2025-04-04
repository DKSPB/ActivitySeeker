using ActivitySeeker.Bll.Models;
using MediatR;

namespace ActivitySeeker.Bll.ActivityType.Queries.GetAll;

public class GetAllActivityTypeQuery : IRequest<List<ActivityTypeDto>>
{
    
}