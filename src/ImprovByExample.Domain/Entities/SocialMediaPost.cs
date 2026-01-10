using ImprovByExample.Domain.Common;
using ImprovByExample.Domain.Enums;

namespace ImprovByExample.Domain.Entities;

public class SocialMediaPost : BaseEntity
{
    public int? ActivityId { get; set; }
    public int? VideoGenerationRequestId { get; set; }
    public SocialMediaPlatform Platform { get; set; }
    public string ContentHash { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
    public string[] Hashtags { get; set; } = Array.Empty<string>();
    public int StatusId { get; set; }
    public DateTime? ScheduledFor { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string? ExternalPostId { get; set; }
    public int? ViewCount { get; set; }
    public int? LikeCount { get; set; }
    public int? ShareCount { get; set; }
    public int? CommentCount { get; set; }

    // Navigation properties
    public ImprovActivity? Activity { get; set; }
    public VideoGenerationRequest? VideoGenerationRequest { get; set; }
    public SocialMediaPostStatus? Status { get; set; }
    public ApplicationUser? CreatedBy { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
}
