using ActivitySeeker.UseCases.ActivityType.Dto;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.UpdateActivityType;

public class UpdateActivityTypeCommand : IRequest
{
    public ActivityTypeDto ActivityTypeDto { get; set; }
}