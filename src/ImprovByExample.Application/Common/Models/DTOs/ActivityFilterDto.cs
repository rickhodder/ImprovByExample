namespace ImprovByExample.Application.Common.Models.DTOs;

public class ActivityFilterDto
{
    public string? SearchTerm { get; set; }
    public int? ActivityTypeId { get; set; }
    public int? ActivitySourceId { get; set; }
    public int? DifficultyId { get; set; }
    public int? MinPlayers { get; set; }
    public int? MaxPlayers { get; set; }
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 20;
    public bool IsPaged => PageSize > 0;
}
