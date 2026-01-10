using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class VideoTimestamp : BaseEntity
{
    public int ExternalVideoReferenceId { get; set; }
    public int TimestampSeconds { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Navigation property
    public ExternalVideoReference ExternalVideoReference { get; set; } = null!;
}
