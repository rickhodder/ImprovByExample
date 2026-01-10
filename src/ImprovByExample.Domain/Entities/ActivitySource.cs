using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class ActivitySource : BaseEntity
{
    public int SourceTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Author { get; set; }
    public string? Url { get; set; }
    public string? AffiliateUrl { get; set; }
    public string? Isbn { get; set; }
    public int? PublishedYear { get; set; }
    public string? Description { get; set; }
    
    // Navigation properties
    public SourceType SourceType { get; set; } = null!;
    public ICollection<ImprovActivity> Activities { get; set; } = new List<ImprovActivity>();
}
