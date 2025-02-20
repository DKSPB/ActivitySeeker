using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivitySeeker.Domain.Seed;

public class ConfigureActivity : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasOne(p => p.ActivityType)
            .WithMany(x => x.Activities)
            .OnDelete(DeleteBehavior.Cascade);
    }
}