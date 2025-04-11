using ActivitySeeker.DataAccess.Interfaces.Infrastructure;
using ApplicationServices.Interfaces;
using AutoMapper;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.UpdateActivityType;

public class UpdateActivityTypeHandler : IRequestHandler<UpdateActivityTypeCommand>
{
    private readonly IMapper _mapper;
    private readonly IActivityTypeService _activityTypeService;

    public UpdateActivityTypeHandler(IMapper mapper, IActivityTypeService activityTypeService)
    {
        _mapper = mapper;
        _activityTypeService = activityTypeService;
    }
    
    public async Task Handle(UpdateActivityTypeCommand command, CancellationToken cancellationToken)
    {
        await _activityTypeService.Update(_mapper.Map<Domain.Entities.ActivityType>(command.UpdateActivityTypeDto), cancellationToken);
    }
}