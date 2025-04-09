using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.DataAccess.Interfaces.Infrastructure;

public interface IDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Activity> Activities { get; }
    public DbSet<ActivityType> ActivityTypes { get; }
    public DbSet<City> Cities { get; }
    public DbSet<Admin> Admins { get; }

    Task<int> SaveChangesAsync(CancellationToken token = default);
}