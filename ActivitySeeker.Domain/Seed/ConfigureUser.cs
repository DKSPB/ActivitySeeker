using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ActivitySeeker.Domain.Seed
{
    public class ConfigureUser : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(e => e.Offer)
                .WithOne()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
