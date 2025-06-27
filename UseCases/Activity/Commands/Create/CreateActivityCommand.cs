using MediatR;

namespace UseCases.Activity.Commands.Create;

public record CreateActivityCommand : IRequest
{
    public Guid UserId { get; init; }
    public Guid ActivityTypeId { get; init; }
    public Guid Description { get; init; } 
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool IsOnline { get; init; }
    public byte[]? Image { get; init; }
    public int? CityId { get; init; }
}