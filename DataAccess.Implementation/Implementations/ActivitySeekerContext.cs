using DataAccess.Seed;
using Domain.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementations
{
    internal class ActivitySeekerContext : DbContext, IDbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Activity> Activities { get; set; } = null!;
        public DbSet<ActivityType> ActivityTypes { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;

        public ActivitySeekerContext(DbContextOptions<ActivitySeekerContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConfigureActivityTypes());
            //modelBuilder.ApplyConfiguration(new ConfigureAccount());
            modelBuilder.ApplyConfiguration(new ConfigureActivity());
        }

        public Task<List<ActivityType>> GetActivityTypeTreeAsync(int parentId)
        {
            throw new NotImplementedException();
        }
    }
}
