using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class SourceType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<ActivitySource> ActivitySources { get; set; } = new List<ActivitySource>();
}
