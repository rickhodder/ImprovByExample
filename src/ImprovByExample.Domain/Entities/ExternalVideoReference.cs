using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class ExternalVideoReference : BaseEntity
{
    public int ActivityId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int VideoPlatformId { get; set; }
    public string AddedById { get; set; } = string.Empty;
    
    // Navigation properties
    public ImprovActivity Activity { get; set; } = null!;
    public VideoPlatform VideoPlatform { get; set; } = null!;
    public ICollection<VideoTimestamp> Timestamps { get; set; } = new List<VideoTimestamp>();
}
