namespace UseCases.Interfaces.Repos
{
    using Domain.Entities;
    public interface IActivityRepository
    {
        Task<Activity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<Activity?> FindAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Activity>> GetAll(ISpecification<Activity> specification, CancellationToken cancellationToken);

        Task CreateAsync(Activity activity, CancellationToken cancellationToken);

        Task<Activity> UpdateAsync(Activity activity, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

