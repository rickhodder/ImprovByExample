namespace ImprovByExample.Application.Common.Models.DTOs;

public class ActivityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ActivityType { get; set; } = string.Empty;
    public string? ActivitySource { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Rules { get; set; } = string.Empty;
    public string? Script { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Difficulty { get; set; }
    public int? MinPlayers { get; set; }
    public int? MaxPlayers { get; set; }
    public int? DurationMinutes { get; set; }
    public string[] Tags { get; set; } = Array.Empty<string>();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
