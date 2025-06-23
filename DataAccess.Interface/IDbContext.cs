using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Interface
{
    public interface IDbContext
    {
        public DbSet<User> Users { get; }
        public DbSet<Activity> Activities { get; }
        public DbSet<ActivityType> ActivityTypes { get; }
        public DbSet<City> Cities { get; }
        public DbSet<Admin> Admins { get; }
        Task<List<ActivityType>> GetActivityTypeTreeAsync(int parentId);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
