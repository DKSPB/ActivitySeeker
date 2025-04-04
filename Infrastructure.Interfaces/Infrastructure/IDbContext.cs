using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Interfaces.Infrastracture;

public interface IDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Activity> Activities { get; }
    public DbSet<ActivityType> ActivityTypes { get; }
    public DbSet<City> Cities { get; }
    public DbSet<Admin> Admins { get; }

    Task<int> SaveChangesAsync(CancellationToken token = default);
}