using DataAccess.Common;
using DataAccess.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UseCases.Common;
using UseCases.Interfaces.Common;
using UseCases.Interfaces.Repos;

namespace DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbContext _context;

    public UserRepository(IDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<User>> GetAllAsync(ISpecification<User> spec, CancellationToken cancellationToken)
    {
        var entities = _context.Users.AsQueryable();

        var total = await entities.CountAsync(cancellationToken);

        var items = spec.Apply(entities);

        return new PagedResult<User>
        {
            Items = items,
            Total = total
        };
    }
    public async Task<User> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) 
               ?? throw new ObjectNotFoundException(nameof(Activity), id);
    }

    public async Task<User?> FindAsync(long id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken);
    }

    public async Task CreateAsync(User activityType, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(activityType, cancellationToken);
    }

    public Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        _context.Users.Remove(entity);
    }
}