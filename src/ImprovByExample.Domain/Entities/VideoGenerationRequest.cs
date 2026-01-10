using ImprovByExample.Domain.Common;

namespace ImprovByExample.Domain.Entities;

public class VideoGenerationRequest : BaseEntity
{
    public int ActivityId { get; set; }
    public string RequestedById { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public string? VideoUrl { get; set; }
    public string? ErrorMessage { get; set; }
    public int Progress { get; set; }
    public DateTime? CompletedAt { get; set; }

    // Navigation properties
    public ImprovActivity? Activity { get; set; }
    public ApplicationUser? RequestedBy { get; set; }
    public VideoGenerationStatus? Status { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
}
