using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ActivitySeeker.Domain.Seed;

public class ConfigureTransition : IEntityTypeConfiguration<TransitionEntity>
{
    public void Configure(EntityTypeBuilder<TransitionEntity> builder)
    {
        builder
            .HasOne(x => x.FromState)
            .WithMany(z => z.OutgoingTransitions) 
            .HasForeignKey(x => x.FromStateId) 
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.ToState)
            .WithMany(z => z.IncomingTransitions)
            .HasForeignKey(x => x.ToStateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}