using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class VideoPlatform : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Navigation properties
    public ICollection<ExternalVideoReference> VideoReferences { get; set; } = new List<ExternalVideoReference>();
}
