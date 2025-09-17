using UseCases.Common;
using UseCases.Interfaces.Common;

namespace UseCases.Interfaces.Repos
{
    using Domain.Entities;
    
    public interface IActivityTypeRepository
    {
        Task<PagedResult<ActivityType>> GetAllAsync(ISpecification<ActivityType> spec, CancellationToken cancellationToken);
        Task<ActivityType> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken);

        Task CreateAsync(ActivityType activityType, CancellationToken cancellationToken);

        Task<ActivityType> UpdateAsync(ActivityType activityType, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

