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
    }
    
    public void SeedCityData()
    {
        if (!Cities.Any())
        {
            var sqlFilePath = "City.sql";

            if (File.Exists(sqlFilePath))
            {
                var sql = File.ReadAllText(sqlFilePath);
                
                Database.ExecuteSqlRaw(sql);
            }
        }
    }
}
    

    

    

    

    