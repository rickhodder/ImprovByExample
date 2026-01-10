using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Infrastructure.Data;

public class ImprovDbContext : IdentityDbContext<ApplicationUser>
{
    public ImprovDbContext(DbContextOptions<ImprovDbContext> options) : base(options)
    {
    }

    public DbSet<ImprovActivity> Activities => Set<ImprovActivity>();
    public DbSet<ActivityType> ActivityTypes => Set<ActivityType>();
    public DbSet<ActivitySource> ActivitySources => Set<ActivitySource>();
    public DbSet<Difficulty> Difficulties => Set<Difficulty>();
    public DbSet<RelationshipType> RelationshipTypes => Set<RelationshipType>();
    public DbSet<ExternalVideoReference> ExternalVideoReferences => Set<ExternalVideoReference>();
    public DbSet<VideoTimestamp> VideoTimestamps => Set<VideoTimestamp>();
    public DbSet<ActivityRelationship> ActivityRelationships => Set<ActivityRelationship>();
    public DbSet<VideoGenerationStatus> VideoGenerationStatuses => Set<VideoGenerationStatus>();
    public DbSet<VideoGenerationRequest> VideoGenerationRequests => Set<VideoGenerationRequest>();
    public DbSet<Show> Shows => Set<Show>();
    public DbSet<ShowActivity> ShowActivities => Set<ShowActivity>();
    public DbSet<SocialMediaPostStatus> SocialMediaPostStatuses => Set<SocialMediaPostStatus>();
    public DbSet<SocialMediaPost> SocialMediaPosts => Set<SocialMediaPost>();
    public DbSet<SocialMediaPostTemplate> SocialMediaPostTemplates => Set<SocialMediaPostTemplate>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Required for Identity

        builder.ApplyConfigurationsFromAssembly(typeof(ImprovDbContext).Assembly);
    }
}
