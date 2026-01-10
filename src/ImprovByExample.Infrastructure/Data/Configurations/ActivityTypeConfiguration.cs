using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Infrastructure.Data.Configurations;

public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
{
    public void Configure(EntityTypeBuilder<ActivityType> builder)
    {
        builder.HasKey(at => at.Id);

        builder.Property(at => at.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(at => at.CreatedBy)
            .WithMany()
            .HasForeignKey(at => at.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(at => at.UpdatedBy)
            .WithMany()
            .HasForeignKey(at => at.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(at => at.Name).IsUnique();
    }
}
