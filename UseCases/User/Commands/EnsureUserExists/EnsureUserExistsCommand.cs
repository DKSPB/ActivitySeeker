using MediatR;

namespace UseCases.User.Commands.EnsureUserExists;

public record EnsureUserExistsCommand(long UserId) : IRequest;