using ImprovByExample.Domain.Common;
using ImprovByExample.Domain.Enums;

namespace ImprovByExample.Domain.Entities;

public class ActivitySource : BaseEntity
{
    public SourceType SourceType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Author { get; set; }
    public string? Url { get; set; }
    public string? AffiliateUrl { get; set; }
    public string? Isbn { get; set; }
    public int? PublishedYear { get; set; }
    public string? Description { get; set; }
    
    // Navigation properties
    public ICollection<ImprovActivity> Activities { get; set; } = new List<ImprovActivity>();
}
