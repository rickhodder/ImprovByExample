using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Common.Models.Responses;

namespace ImprovByExample.Application.Common.Interfaces.Services;

public interface IActivityService
{
    Task<PagedResult<ActivityDto>> GetActivitiesAsync(ActivityFilterDto filter, CancellationToken cancellationToken = default);
    Task<ActivityDto?> GetActivityByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<ActivityDto> CreateActivityAsync(CreateActivityDto dto, string createdById, CancellationToken cancellationToken = default);
    Task<ActivityDto?> UpdateActivityAsync(UpdateActivityDto dto, string updatedById, CancellationToken cancellationToken = default);
    Task<bool> DeleteActivityAsync(int id, CancellationToken cancellationToken = default);
}
