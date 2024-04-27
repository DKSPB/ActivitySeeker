using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.Domain;

public class ActivitySeekerContext: DbContext
{
    //public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<Activity> Activities { get; set; } = null!;
    
    public DbSet<ActivityType> ActivityTypes { get; set; } = null!;
    
    public ActivitySeekerContext(DbContextOptions<ActivitySeekerContext> options) : base(options)
    {
    }
    
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
        //modelBuilder.ApplyConfiguration(new ConfigureUsers());
        //modelBuilder.ApplyConfiguration(new ConfigureAccounts());
        //modelBuilder.ApplyConfiguration(new ConfigureUserAccounts());
    //}
}
    

    

    

    

    