using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UseCases.Interfaces;

namespace Auth
{
    public abstract class BaseAuthorization<TEntity, TId> : IEntityAuthorization<TEntity, TId> where TEntity : class
    {
        protected readonly IDbContext _context;

        protected BaseAuthorization(IDbContext context)
        {
            _context = context;
        }

        protected abstract DbSet<TEntity> Set { get; }
        protected abstract Expression<Func<TEntity, bool>> IsOwnerPredicate(TId id, long userId);

        public async Task EnsureCanModifyAsync(TId entityId, long userId, CancellationToken ct)
        {
            var canModify = await Set
                .AsNoTracking()
                .AnyAsync(IsOwnerPredicate(entityId, userId), ct);

            if (!canModify)
            {
                throw new UnauthorizedAccessException(
                    $"Недостаточно прав для изменения {typeof(TEntity).Name} [{entityId}].");
            }      
        }

    }
}
