using ImprovByExample.Domain.Common;
using ImprovByExample.Domain.Enums;

namespace ImprovByExample.Domain.Entities;

public class ExternalVideoReference : BaseEntity
{
    public int ActivityId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public VideoPlatform Platform { get; set; }

    // Navigation properties
    public ImprovActivity? Activity { get; set; }
    public ApplicationUser? AddedBy { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
    public ICollection<VideoTimestamp> VideoTimestamps { get; set; } = new List<VideoTimestamp>();
}
