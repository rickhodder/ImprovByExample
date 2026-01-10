using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Infrastructure.Data.Configurations;

public class ActivitySourceConfiguration : IEntityTypeConfiguration<ActivitySource>
{
    public void Configure(EntityTypeBuilder<ActivitySource> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.Author)
            .HasMaxLength(200);

        builder.Property(s => s.Url)
            .HasMaxLength(500);

        builder.Property(s => s.AffiliateUrl)
            .HasMaxLength(500);

        builder.Property(s => s.Isbn)
            .HasMaxLength(20);

        builder.HasOne(s => s.CreatedBy)
            .WithMany()
            .HasForeignKey(s => s.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.UpdatedBy)
            .WithMany()
            .HasForeignKey(s => s.UpdatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(s => s.Name);
    }
}
