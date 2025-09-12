namespace UseCases.Interfaces.Repos
{
    using Domain.Entities;
    
    public interface IActivityTypeRepository
    {
        Task<ActivityType> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<bool> AnyAsync(Guid id, CancellationToken cancellationToken);

        Task CreateAsync(ActivityType activityType, CancellationToken cancellationToken);

        Task<ActivityType> UpdateAsync(ActivityType activityType, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}

