using DataAccess.Interfaces;
using MediatR;

namespace UseCases.User.Commands.EnsureUserExists;

internal class EnsureUserExistsHandler : IRequestHandler<EnsureUserExistsCommand>
{
    private readonly IDbContext _context;

    public EnsureUserExistsHandler(IDbContext context)
    {
        _context = context;
    }
        
    public async Task Handle(EnsureUserExistsCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FindAsync(new object?[] {command.UserId }, cancellationToken);

        if (entity is null)
        {
            await _context.Users.AddAsync(new Domain.Entities.User {Id = command.UserId}, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}