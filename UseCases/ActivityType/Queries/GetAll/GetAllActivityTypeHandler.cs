using MediatR;
using ActivitySeeker.UseCases.ActivityType.Dto;
using ApplicationServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.UseCases.ActivityType.Queries.GetAll;

public class GetAllActivityTypeHandler: IRequestHandler<GetAllActivityTypeQuery, List<ActivityTypeDto>>
{
    private readonly IActivityTypeService _activityTypeService;

    public GetAllActivityTypeHandler(IActivityTypeService activityTypeService)
    {
        _activityTypeService = activityTypeService;
    }
    
    public async Task<List<ActivityTypeDto>> Handle(GetAllActivityTypeQuery request, CancellationToken cancellationToken)
    {
        return await _activityTypeService.GetAll()
            .Select(x => new ActivityTypeDto(x))
            .ToListAsync(cancellationToken);
    }
}