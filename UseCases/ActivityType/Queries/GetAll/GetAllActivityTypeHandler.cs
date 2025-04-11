using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ApplicationServices.Interfaces;
using ActivitySeeker.UseCases.ActivityType.Dto;

namespace ActivitySeeker.UseCases.ActivityType.Queries.GetAll;

public class GetAllActivityTypeHandler : IRequestHandler<GetAllActivityTypeQuery, List<ActivityTypeViewModel>>
{
    private readonly IMapper _mapper;
    private readonly IActivityTypeService _activityTypeService;

    public GetAllActivityTypeHandler(IActivityTypeService activityTypeService, IMapper mapper)
    {
        _mapper = mapper;
        _activityTypeService = activityTypeService;
    }
    
    public async Task<List<ActivityTypeViewModel>> Handle(GetAllActivityTypeQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<ActivityTypeViewModel>>(await _activityTypeService.GetAll()
            .ToListAsync(cancellationToken));
    }
}