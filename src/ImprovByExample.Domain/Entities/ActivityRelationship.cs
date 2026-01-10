using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class ActivityRelationship : BaseEntity
{
    public int ActivityId { get; set; }
    public int RelatedActivityId { get; set; }
    public int RelationshipTypeId { get; set; }
    public string? Notes { get; set; }

    // Navigation properties
    public ImprovActivity? Activity { get; set; }
    public ImprovActivity? RelatedActivity { get; set; }
    public RelationshipType? RelationshipType { get; set; }
    public ApplicationUser? CreatedBy { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
}
