using ActivitySeeker.Bll.Models;
using Infrastructure.Interfaces.Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.Bll.ActivityType.Queries.GetById;

public class GetActivityTypeByIdQueryHandler : IRequestHandler<GetActivityTypeByIdQuery, ActivityTypeDto>
{
    private readonly IDbContext _context;

    public GetActivityTypeByIdQueryHandler(IDbContext context)
    {
        _context = context;
    }
    
    public async  Task<ActivityTypeDto> Handle(GetActivityTypeByIdQuery request, CancellationToken cancellationToken)
    {
        //var activityTypeEntity = await GetActivityTypes().FirstOrDefaultAsync(x => x.Id == id);

        var activityTypeEntity = await _context.ActivityTypes
            .FirstOrDefaultAsync(x => x.Id == request.ActivityTypeId, cancellationToken);

        if (activityTypeEntity is null)
        {
            throw new NullReferenceException($"Тип активности с идентификатором {request.ActivityTypeId} не найден");
        }
 
        return new ActivityTypeDto(activityTypeEntity);
    }
}