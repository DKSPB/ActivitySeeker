using ActivitySeeker.UseCases.ActivityType.Dto;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.CreateActivityType;

public class CreateActivityTypeCommand: IRequest
{
    public CreateActivityTypeCommand(CreateActivityTypeDto createActivityTypeDto)
    {
        CreateActivityTypeDto = createActivityTypeDto;
    }

    public CreateActivityTypeDto CreateActivityTypeDto { get; }
}