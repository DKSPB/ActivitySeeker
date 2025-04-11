using ApplicationServices.Interfaces;
using AutoMapper;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.CreateActivityType;

public class CreateActivityTypeCommandHandler : IRequestHandler<CreateActivityTypeCommand>
{
    private readonly IMapper _mapper;
    private readonly IActivityTypeService _activityTypeService;
    public CreateActivityTypeCommandHandler(IMapper mapper, IActivityTypeService activityTypeService)
    {
        _mapper = mapper;
        _activityTypeService = activityTypeService;
    }
    public async Task Handle(CreateActivityTypeCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.ActivityType>(command.CreateActivityTypeDto);
        await _activityTypeService.Create(entity, cancellationToken);
    }
}