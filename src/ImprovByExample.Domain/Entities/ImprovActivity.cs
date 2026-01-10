using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class ImprovActivity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int ActivityTypeId { get; set; }
    public int? ActivitySourceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Rules { get; set; } = string.Empty;
    public string? Script { get; set; }
    public string Category { get; set; } = string.Empty;
    public int? DifficultyId { get; set; }
    public int? MinPlayers { get; set; }
    public int? MaxPlayers { get; set; }
    public int? DurationMinutes { get; set; }
    public string[] Tags { get; set; } = Array.Empty<string>();

    // Navigation properties
    public ActivityType? ActivityType { get; set; }
    public ActivitySource? ActivitySource { get; set; }
    public Difficulty? Difficulty { get; set; }
    public ApplicationUser? CreatedBy { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
    
    public ICollection<ExternalVideoReference> ExternalVideoReferences { get; set; } = new List<ExternalVideoReference>();
    public ICollection<ActivityRelationship> ActivityRelationships { get; set; } = new List<ActivityRelationship>();
    public ICollection<ActivityRelationship> RelatedActivityRelationships { get; set; } = new List<ActivityRelationship>();
    public ICollection<VideoGenerationRequest> VideoGenerationRequests { get; set; } = new List<VideoGenerationRequest>();
    public ICollection<ShowActivity> ShowActivities { get; set; } = new List<ShowActivity>();
}
