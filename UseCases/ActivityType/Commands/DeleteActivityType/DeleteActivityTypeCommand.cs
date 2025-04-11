using MediatR;

namespace ActivitySeeker.UseCases.ActivityType.Commands.DeleteActivityType;

public class DeleteActivityTypeCommand : IRequest
{
    public DeleteActivityTypeCommand(List<Guid> activityTypesForDelete)
    {
        ActivityTypesForDelete = activityTypesForDelete;
    }

    public List<Guid> ActivityTypesForDelete { get; }
}