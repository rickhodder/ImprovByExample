using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class ActivityType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ApplicationUser? CreatedBy { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
    public ICollection<ImprovActivity> Activities { get; set; } = new List<ImprovActivity>();
}
