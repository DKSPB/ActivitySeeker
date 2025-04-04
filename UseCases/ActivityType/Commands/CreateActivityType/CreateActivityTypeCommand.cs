using ActivitySeeker.Bll.Models;
using MediatR;

namespace UseCases.ActivityType.Commands.CreateActivityType;

public class CreateActivityTypeCommand: IRequest
{
    public ActivityTypeDto ActivityTypeDto { get; set; }
}