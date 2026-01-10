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
    public ActivityType ActivityType { get; set; } = null!;
    public ActivitySource? ActivitySource { get; set; }
    public Difficulty? Difficulty { get; set; }
    public ICollection<ExternalVideoReference> VideoReferences { get; set; } = new List<ExternalVideoReference>();
    public ICollection<ActivityRelationship> RelatedActivities { get; set; } = new List<ActivityRelationship>();
    public ICollection<ActivityRelationship> RelatedByActivities { get; set; } = new List<ActivityRelationship>();
}
