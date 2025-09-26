namespace DataAccess.Repositories
{
    using Interfaces;
    using Domain.Entities;
    using UseCases.Common;
    using UseCases.Interfaces.Repos;
    using UseCases.Interfaces.Common;
    using Microsoft.EntityFrameworkCore;

    public class ActivityTypeRepository : IActivityTypeRepository
    {
        private readonly IDbContext _context;
    
        public ActivityTypeRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ActivityType>> GetAllAsync(ISpecification<ActivityType> spec, CancellationToken cancellationToken)
        {
            var entities = _context.ActivityTypes.AsQueryable();
            
            var total = await entities.CountAsync(cancellationToken);

            var items = await spec.Apply(entities).ToListAsync(cancellationToken);;

            return new PagedResult<ActivityType>
            {
                Items = items,
                Total = total
            };
        }

        public async Task<ActivityType> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.ActivityTypes
                       .FindAsync(new object?[] { id }, cancellationToken: cancellationToken) ?? 
                   throw new NullReferenceException($"Тип активности с идентификатором {id} не найден");
        }

        public async Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.ActivityTypes.AnyAsync(type => type.Id == id, cancellationToken);
        }

        public async Task CreateAsync(ActivityType activityType, CancellationToken cancellationToken)
        {
            await _context.ActivityTypes.AddAsync(activityType, cancellationToken);
        }

        public async Task<ActivityType> UpdateAsync(ActivityType activityType, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(activityType.Id, cancellationToken);
            
            entity.TypeName = activityType.TypeName;
            entity.ParentId = activityType.ParentId;

            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            _context.ActivityTypes.Remove(entity);
        }
    }
}

