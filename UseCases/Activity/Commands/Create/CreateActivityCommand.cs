using MediatR;

namespace UseCases.Activity.Commands.Create;

public record CreateActivityCommand : IRequest
{
    public long AuthorVkId { get; init; }
    public Guid ActivityTypeId { get; init; }
    public Guid LinkOrDescription { get; init; } 
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public bool IsOnline { get; init; }
    public byte[]? Image { get; init; }
    public int? CityId { get; init; }
}