using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using UseCases.User.Models;

namespace UseCases.User.Commands.EnsureUserExists;

internal class EnsureUserExistsHandler : IRequestHandler<EnsureUserExistsCommand, (bool, UserDto)>
{
    private readonly IMapper _mapper;
    private readonly IDbContext _context;

    public EnsureUserExistsHandler(IMapper mapper, IDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
        
    public async Task<(bool, UserDto)> Handle(EnsureUserExistsCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Users.FindAsync(new object?[] {command.UserId }, cancellationToken);
        
        return entity is null ? (true, await CreateUser(command.UserId, cancellationToken)) :
            (false, new UserDto{ Id = entity.Id});
    }

    private async Task<UserDto> CreateUser(long userId, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.User { Id = userId };
        
        await _context.Users.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new UserDto {Id = entity.Id};
    }
}