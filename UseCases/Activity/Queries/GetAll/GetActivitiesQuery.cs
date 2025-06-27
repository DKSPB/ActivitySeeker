using MediatR;
using UseCases.Activity.Models;
using UseCases.Common;

namespace UseCases.Activity.Queries.GetAll;

public record GetActivitiesQuery : IRequest<PagedResult<ActivityDto>>
{
    public int Limit { get; init; } = 20;

    public int Offset { get; init; } = 1;
    
    public Guid? ActivityTypeId { get; init; }
    
    public DateTime? SearchFrom { get; init; }
    
    public DateTime? SearchTo { get; init; }

    public bool? IsOnline { get; init; }

    public bool? IsPublished { get; init; }

    public int? CityId { get; init; }
}