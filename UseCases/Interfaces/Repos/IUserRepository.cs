namespace UseCases.Interfaces.Repos
{
    using Common;
    using UseCases.Common;
    using Domain.Entities;

    public interface IUserRepository
    {
        Task<PagedResult<User>> GetAllAsync(ISpecification<User> spec, CancellationToken cancellationToken);
    
        Task<User> GetByIdAsync(long id, CancellationToken cancellationToken);

        Task<User?> FindAsync(long id, CancellationToken cancellationToken);

        Task CreateAsync(User user, CancellationToken cancellationToken);

        Task<User> UpdateAsync(User user, CancellationToken cancellationToken);

        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}