using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Infrastructure.Data.Configurations;

public class ImprovActivityConfiguration : IEntityTypeConfiguration<ImprovActivity>
{
    public void Configure(EntityTypeBuilder<ImprovActivity> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(a => a.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(a => a.Rules)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(a => a.Category)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Tags)
            .HasColumnType("text[]");

        builder.HasOne(a => a.ActivityType)
            .WithMany(at => at.Activities)
            .HasForeignKey(a => a.ActivityTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.ActivitySource)
            .WithMany(s => s.Activities)
            .HasForeignKey(a => a.ActivitySourceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Difficulty)
            .WithMany(d => d.Activities)
            .HasForeignKey(a => a.DifficultyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.CreatedBy)
            .WithMany()
            .HasForeignKey(a => a.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.UpdatedBy)
            .WithMany()
            .HasForeignKey(a => a.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => a.Name);
        builder.HasIndex(a => a.Category);
    }
}
