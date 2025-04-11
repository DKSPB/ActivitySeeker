using ApplicationServices.Interfaces;
using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.DeleteActivityType;

public class DeleteActivityTypeCommandHandler : IRequestHandler<DeleteActivityTypeCommand>
{
    private readonly IActivityTypeService _activityTypeService;

    public DeleteActivityTypeCommandHandler(IActivityTypeService activityTypeService)
    {
        _activityTypeService = activityTypeService;
    }
    
    public async Task Handle(DeleteActivityTypeCommand command, CancellationToken cancellationToken)
    {
        await _activityTypeService.Delete(command.ActivityTypesForDelete, cancellationToken);
    }
}