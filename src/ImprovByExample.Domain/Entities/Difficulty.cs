using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class Difficulty : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<ImprovActivity> Activities { get; set; } = new List<ImprovActivity>();
}
