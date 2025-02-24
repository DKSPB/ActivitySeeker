using ActivitySeeker.Domain.Entities;
using ActivitySeeker.Domain.Seed;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.Domain;

public class ActivitySeekerContext: DbContext
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
        modelBuilder.ApplyConfiguration(new ConfigureActivity());
        modelBuilder.ApplyConfiguration(new ConfigureUser());
    }
}
    

    

    

    

    