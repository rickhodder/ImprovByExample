using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Infrastructure.Data.Configurations;

public class ActivityRelationshipConfiguration : IEntityTypeConfiguration<ActivityRelationship>
{
    public void Configure(EntityTypeBuilder<ActivityRelationship> builder)
    {
        builder.HasKey(ar => ar.Id);

        builder.HasOne(ar => ar.Activity)
            .WithMany(a => a.ActivityRelationships)
            .HasForeignKey(ar => ar.ActivityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ar => ar.RelatedActivity)
            .WithMany(a => a.RelatedActivityRelationships)
            .HasForeignKey(ar => ar.RelatedActivityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ar => ar.RelationshipType)
            .WithMany(rt => rt.ActivityRelationships)
            .HasForeignKey(ar => ar.RelationshipTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ar => ar.CreatedBy)
            .WithMany()
            .HasForeignKey(ar => ar.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ar => ar.UpdatedBy)
            .WithMany()
            .HasForeignKey(ar => ar.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(ar => new { ar.ActivityId, ar.RelatedActivityId, ar.RelationshipTypeId })
            .IsUnique();
    }
}
