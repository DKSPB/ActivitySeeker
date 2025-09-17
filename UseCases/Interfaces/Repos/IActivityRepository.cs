using UseCases.Common;

namespace UseCases.Interfaces.Repos
{
    using Common;
    using Domain.Entities;
    public interface IActivityRepository
    {
        Task<Activity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Activity?> FindAsync(Guid id, CancellationToken cancellationToken);

        Task<PagedResult<Activity>> GetAllAsync(ISpecification<Activity> specification, CancellationToken cancellationToken);

        Task CreateAsync(Activity activity, CancellationToken cancellationToken);

        Task<Activity> UpdateAsync(Activity activity, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

