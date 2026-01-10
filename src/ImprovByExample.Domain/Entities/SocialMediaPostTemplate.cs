using ImprovByExample.Domain.Common;
using ImprovByExample.Domain.Enums;

namespace ImprovByExample.Domain.Entities;

public class SocialMediaPostTemplate : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public SocialMediaPlatform Platform { get; set; }
    public string CaptionTemplate { get; set; } = string.Empty;
    public string[] DefaultHashtags { get; set; } = Array.Empty<string>();
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ApplicationUser? CreatedBy { get; set; }
    public ApplicationUser? UpdatedBy { get; set; }
}
