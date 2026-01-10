using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class ActivityRelationship : BaseEntity
{
    public int ActivityId { get; set; }
    public int RelatedActivityId { get; set; }
    public int RelationshipTypeId { get; set; }
    public string? Notes { get; set; }
    
    // Navigation properties
    public ImprovActivity Activity { get; set; } = null!;
    public ImprovActivity RelatedActivity { get; set; } = null!;
    public RelationshipType RelationshipType { get; set; } = null!;
}
