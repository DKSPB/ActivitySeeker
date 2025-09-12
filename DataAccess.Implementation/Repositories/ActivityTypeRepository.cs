using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    using Interfaces;
    using Domain.Entities;
    using UseCases.Interfaces.Repos;
    public class ActivityTypeRepository : IActivityTypeRepository
    {
        private readonly IDbContext _context;
    
        public ActivityTypeRepository(IDbContext context)
        {
            _context = context;
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

