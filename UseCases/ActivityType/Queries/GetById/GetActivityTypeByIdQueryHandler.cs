using ActivitySeeker.UseCases.ActivityType.Dto;
using ApplicationServices.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.UseCases.ActivityType.Queries.GetById;

public class GetActivityTypeByIdQueryHandler : IRequestHandler<GetActivityTypeByIdQuery, ActivityTypeDto>
{
    private readonly IActivityTypeService _activityTypeService;

    public GetActivityTypeByIdQueryHandler(IActivityTypeService activityTypeService)
    {
        _activityTypeService = activityTypeService;
    }
    
    public async Task<ActivityTypeDto> Handle(GetActivityTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var activityTypeEntity = await _activityTypeService.GetAll()
            .FirstOrDefaultAsync(x => x.Id == request.ActivityTypeId, cancellationToken);

        if (activityTypeEntity is null)
        {
            throw new NullReferenceException($"Тип активности с идентификатором {request.ActivityTypeId} не найден");
        }
 
        return new ActivityTypeDto(activityTypeEntity);
    }
}