using MediatR;
using UseCases.User.Models;

namespace UseCases.User.Commands.EnsureUserExists;

public record EnsureUserExistsCommand(long UserId) : IRequest<(bool, UserDto)>;