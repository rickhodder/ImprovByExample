namespace ImprovByExample.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedById { get; set; } = string.Empty;
    public string? UpdatedById { get; set; }
}
