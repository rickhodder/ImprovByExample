namespace ImprovByExample.Application.Common.Models.DTOs;

public class CreateActivityDto
{
    public string Name { get; set; } = string.Empty;
    public int ActivityTypeId { get; set; }
    public int? ActivitySourceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Rules { get; set; } = string.Empty;
    public string? Script { get; set; }
    public string Category { get; set; } = string.Empty;
    public int? DifficultyId { get; set; }
    public int? MinPlayers { get; set; }
    public int? MaxPlayers { get; set; }
    public int? DurationMinutes { get; set; }
    public string[] Tags { get; set; } = Array.Empty<string>();
}
