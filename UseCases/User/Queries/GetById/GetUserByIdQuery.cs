using MediatR;
using UseCases.User.Models;

namespace UseCases.User.Queries.GetById
{
    public record GetUserByIdQuery(long UserId) : IRequest<UserDto>;
}
