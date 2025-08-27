using MediatR;
using UseCases.Common;
using UseCases.Activity.Models;


namespace UseCases.Activity.Queries.GetAll;

public class GetActivitiesQuery : IRequest<PagedResult<ActivityDto>>
{
    public int Limit { get; set; } = 20; 
    public int Offset { get; set; } = 1;
    public Guid? ActivityTypeId { get; init; }
    public long? UserId { get; set; }
    public DateTime? SearchFrom { get; set; }
    public DateTime? SearchTo { get; set; }
    public int Timestamp { get; set; } = 3;
    public bool? IsOnline { get; init; }
    public bool? IsPublished { get; init; }
    public int? CityId { get; init; }
}