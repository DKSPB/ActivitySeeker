using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivitySeeker.Domain.Seed
{
    internal class ConfigureActivityTypes : IEntityTypeConfiguration<ActivityType>
    {
        public void Configure(EntityTypeBuilder<ActivityType> builder)
        {
            builder.HasOne(e => e.Parent)
                .WithMany(x => x.Children)
                .OnDelete(DeleteBehavior.SetNull);
            
            List<ActivityType> activityTypes = new()
            {
                new ActivityType
                {
                    Id = Guid.Parse("34f4633c-13d8-478b-bb9a-83396e04e48d"),
                    TypeName = "События на открытом воздухе",
                },
                new ActivityType
                {
                    Id = Guid.Parse("2a0c9a0f-3f73-4572-a9fd-39c503135f29"),
                    TypeName = "Хобби"
                },
                new ActivityType()
                {
                    Id = Guid.Parse("fd689706-6407-4665-a982-e39e4db3c608"),
                    TypeName = "Мастер-классы"
                }
            };

            builder.HasData(activityTypes);
        }
    }
}
