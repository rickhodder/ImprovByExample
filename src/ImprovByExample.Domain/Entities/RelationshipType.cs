using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class RelationshipType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Navigation properties
    public ICollection<ActivityRelationship> ActivityRelationships { get; set; } = new List<ActivityRelationship>();
}
