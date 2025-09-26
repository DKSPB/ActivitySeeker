namespace DataAccess.Repositories
{
    using Interfaces;
    using UseCases.Interfaces.Repos;
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _context;
    
        public UnitOfWork(IDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

