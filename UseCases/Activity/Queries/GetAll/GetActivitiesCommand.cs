using MediatR;
using UseCases.Activity.Models;

namespace UseCases.Activity.Queries.GetAll;

public record GetActivitiesCommand : IRequest<List<ActivityDto>>
{
    public int Limit { get; init; } = 20;
    
    public int Offset { get; init; }
    
    public Guid? ActivityTypeId { get; init; }
    
    public DateTime? SearchFrom { get; init; }
    
    public DateTime? SearchTo { get; init; }

    public bool? IsOnline { get; init; }

    public bool? IsPublished { get; init; }

    public int? CityId { get; init; }
}