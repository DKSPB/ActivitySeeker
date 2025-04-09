using ActivitySeeker.UseCases.ActivityType.Dto;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.CreateActivityType;

public class CreateActivityTypeCommand: IRequest
{
    public ActivityTypeDto ActivityTypeDto { get; set; }
}