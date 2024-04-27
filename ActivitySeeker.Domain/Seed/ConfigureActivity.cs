using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivitySeeker.Domain.Seed;

public class ConfigureActivity
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        List<Activity> activities = new()
        {
            new Activity
            {
                Id = Guid.Parse("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                Description = "Мастер классы",
                StartDate = DateTime.Now
            },
            new Activity
            {
                Id = Guid.Parse("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                Description = "Настольные игры",
                StartDate = DateTime.Now
            },

            new Activity()
            {
                Id = Guid.Parse("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                Description = "Прогулки на велосипедах",
                StartDate = DateTime.Now
            },

            new Activity()
            {
                Id = Guid.Parse("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                Description = "Мастер классы",
                StartDate = DateTime.Now
            },

            new Activity()
            {
                Id = Guid.Parse("66353503-2ad9-4e90-ae0c-4b46a69b6481"),
                Description = "Мастер классы",
                StartDate = DateTime.Now
            }
        };

        builder.HasData(activities);
    }
}