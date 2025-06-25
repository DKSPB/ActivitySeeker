using MediatR;

namespace UseCases.Activity.Commands.Update;

public class UpdateActivityCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid ActivityTypeId { get; init; }
    public string LinkOrDescription { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool IsOnline { get; init; }
    public byte[]? Image { get; init; }
    public int? CityId { get; init; }
}