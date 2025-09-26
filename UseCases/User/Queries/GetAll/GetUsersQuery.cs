using MediatR;
using UseCases.Common;
using UseCases.User.Models;

namespace UseCases.User.Queries.GetAll
{
    public record GetUsersQuery(int Limit = 20, int Offset = 1) : IRequest<PagedResult<UserDto>>;
}
