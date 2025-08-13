using UseCases.Activity.Commands.Create;

namespace UseCases.Activity.Commands.Update;

public class UpdateActivityCommand : CreateActivityCommand
{
    public Guid Id { get; set; }
}