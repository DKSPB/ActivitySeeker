using ActivitySeeker.UseCases.ActivityType.Dto;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.UpdateActivityType;

public class UpdateActivityTypeCommand : IRequest
{
    public UpdateActivityTypeCommand(UpdateActivityTypeDto updateActivityTypeDto)
    {
        UpdateActivityTypeDto = updateActivityTypeDto;
    }

    public UpdateActivityTypeDto UpdateActivityTypeDto { get; }
}