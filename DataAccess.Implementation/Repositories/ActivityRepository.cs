namespace DataAccess.Repositories
{
    using Common;
    using Interfaces;
    using Domain.Entities;
    using UseCases.Common;
    using UseCases.Interfaces.Repos;
    using UseCases.Interfaces.Common;
    using Microsoft.EntityFrameworkCore;
    public class ActivityRepository : IActivityRepository
    {
        private readonly IDbContext _context;
        
        public ActivityRepository(IDbContext context)
        {
            _context = context;
        }
        public async Task<Activity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Activities.Include(x => x.ActivityType)
                             .FirstAsync(x => x.Id == id, cancellationToken: cancellationToken) ?? 
                         throw new ObjectNotFoundException(nameof(Activity), id);
        }

        public async Task<Activity?> FindAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Activities.FindAsync(
                new object?[] { id, cancellationToken }, cancellationToken: cancellationToken);
        }

        public async Task<PagedResult<Activity>> GetAllAsync(ISpecification<Activity> specification,
            CancellationToken cancellationToken)
        {
            var entities = _context.Activities.AsQueryable();

            var total = await entities.CountAsync(cancellationToken);

            var items = specification.Apply(entities);

            return new PagedResult<Activity>
            {
                Items = items,
                Total = total
            };
        }
        public async Task CreateAsync(Activity activity, CancellationToken cancellationToken)
        {
            await _context.Activities.AddAsync(activity, cancellationToken);
        }

        public async Task<Activity> UpdateAsync(Activity activity, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(activity.Id, cancellationToken);

            entity.ActivityTypeId = activity.ActivityTypeId;
            entity.Description = activity.Description;
            entity.StartDate = activity.StartDate;
            entity.EndDate = activity.EndDate;
            entity.Timezone = activity.Timezone;
            entity.IsOnline = activity.IsOnline;
            entity.CityId = activity.CityId;

            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            _context.Activities.Remove(entity);
        }
    }
}