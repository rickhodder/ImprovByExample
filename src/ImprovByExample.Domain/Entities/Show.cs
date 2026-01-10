using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class Show : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    // Navigation properties
    public ApplicationUser? CreatedBy { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
    public ICollection<ShowActivity> ShowActivities { get; set; } = new List<ShowActivity>();
}
