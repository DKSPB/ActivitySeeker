using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Seed
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
