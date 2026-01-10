using ImprovByExample.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImprovByExample.Infrastructure.Data;

public class ImprovDbContext : IdentityDbContext<ApplicationUser>
{
    public ImprovDbContext(DbContextOptions<ImprovDbContext> options) : base(options)
    {
    }

    public DbSet<ImprovActivity> Activities { get; set; } = null!;
    public DbSet<ActivityType> ActivityTypes { get; set; } = null!;
    public DbSet<ActivitySource> ActivitySources { get; set; } = null!;
    public DbSet<SourceType> SourceTypes { get; set; } = null!;
    public DbSet<VideoPlatform> VideoPlatforms { get; set; } = null!;
    public DbSet<Difficulty> Difficulties { get; set; } = null!;
    public DbSet<RelationshipType> RelationshipTypes { get; set; } = null!;
    public DbSet<ExternalVideoReference> VideoReferences { get; set; } = null!;
    public DbSet<VideoTimestamp> VideoTimestamps { get; set; } = null!;
    public DbSet<ActivityRelationship> ActivityRelationships { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Required for Identity

        // Configure ImprovActivity
        builder.Entity<ImprovActivity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired();
            entity.Property(e => e.Rules).IsRequired();
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.CreatedById).IsRequired();
            
            // Configure array properties for PostgreSQL
            entity.Property(e => e.Tags).HasColumnType("text[]");

            // Relationships
            entity.HasOne(e => e.ActivityType)
                .WithMany(t => t.Activities)
                .HasForeignKey(e => e.ActivityTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ActivitySource)
                .WithMany(s => s.Activities)
                .HasForeignKey(e => e.ActivitySourceId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            entity.HasOne(e => e.Difficulty)
                .WithMany(d => d.Activities)
                .HasForeignKey(e => e.DifficultyId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        });

        // Configure ActivityType
        builder.Entity<ActivityType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.CreatedById).IsRequired();
        });

        // Configure ActivitySource
        builder.Entity<ActivitySource>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Author).HasMaxLength(200);
            entity.Property(e => e.Url).HasMaxLength(500);
            entity.Property(e => e.AffiliateUrl).HasMaxLength(500);
            entity.Property(e => e.Isbn).HasMaxLength(20);
            entity.Property(e => e.CreatedById).IsRequired();

            entity.HasOne(e => e.SourceType)
                .WithMany(st => st.ActivitySources)
                .HasForeignKey(e => e.SourceTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure SourceType
        builder.Entity<SourceType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.CreatedById).IsRequired();
        });

        // Configure VideoPlatform
        builder.Entity<VideoPlatform>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.CreatedById).IsRequired();
        });

        // Configure Difficulty
        builder.Entity<Difficulty>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedById).IsRequired();
        });

        // Configure RelationshipType
        builder.Entity<RelationshipType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.CreatedById).IsRequired();
        });

        // Configure ExternalVideoReference
        builder.Entity<ExternalVideoReference>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Url).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.AddedById).IsRequired();
            entity.Property(e => e.CreatedById).IsRequired();

            entity.HasOne(e => e.Activity)
                .WithMany(a => a.VideoReferences)
                .HasForeignKey(e => e.ActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.VideoPlatform)
                .WithMany(vp => vp.VideoReferences)
                .HasForeignKey(e => e.VideoPlatformId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure VideoTimestamp
        builder.Entity<VideoTimestamp>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Label).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            entity.Property(e => e.CreatedById).IsRequired();

            entity.HasOne(e => e.ExternalVideoReference)
                .WithMany(v => v.Timestamps)
                .HasForeignKey(e => e.ExternalVideoReferenceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure ActivityRelationship
        builder.Entity<ActivityRelationship>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.Property(e => e.CreatedById).IsRequired();

            entity.HasOne(e => e.Activity)
                .WithMany(a => a.RelatedActivities)
                .HasForeignKey(e => e.ActivityId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.RelatedActivity)
                .WithMany(a => a.RelatedByActivities)
                .HasForeignKey(e => e.RelatedActivityId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.RelationshipType)
                .WithMany(t => t.ActivityRelationships)
                .HasForeignKey(e => e.RelationshipTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure unique relationships
            entity.HasIndex(e => new { e.ActivityId, e.RelatedActivityId, e.RelationshipTypeId })
                .IsUnique();
        });

        // Add indexes for common queries
        builder.Entity<ImprovActivity>()
            .HasIndex(e => e.Name);
        
        builder.Entity<ImprovActivity>()
            .HasIndex(e => e.ActivityTypeId);
        
        builder.Entity<ImprovActivity>()
            .HasIndex(e => e.ActivitySourceId);

        builder.Entity<ExternalVideoReference>()
            .HasIndex(e => e.ActivityId);
        
        builder.Entity<ActivityRelationship>()
            .HasIndex(e => e.ActivityId);
    }
}
