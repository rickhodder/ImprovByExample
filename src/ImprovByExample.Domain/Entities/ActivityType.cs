using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class ActivityType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<ImprovActivity> Activities { get; set; } = new List<ImprovActivity>();
}
