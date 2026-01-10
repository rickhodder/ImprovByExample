using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class ShowActivity : BaseEntity
{
    public int ShowId { get; set; }
    public int ActivityId { get; set; }
    public int OrderIndex { get; set; }
    public string[] Players { get; set; } = Array.Empty<string>();

    // Navigation properties
    public Show? Show { get; set; }
    public ImprovActivity? Activity { get; set; }
    public ApplicationUser? CreatedBy { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
}
