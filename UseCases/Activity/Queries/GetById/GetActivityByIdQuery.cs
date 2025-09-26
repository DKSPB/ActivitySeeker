using MediatR;
using UseCases.Activity.Models;

namespace UseCases.Activity.Queries.GetById
{
    public record GetActivityByIdQuery(Guid Id) : IRequest<ActivityDto>;
}
