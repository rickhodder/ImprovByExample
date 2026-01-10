using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Common.Models.Responses;

namespace ImprovByExample.Application.Common.Interfaces.Services;

public interface IActivityService
{
    Task<ActivityDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedResult<ActivityDto>> SearchAsync(ActivityFilterDto filter, CancellationToken cancellationToken = default);
    Task<ActivityDto> CreateAsync(CreateActivityDto dto, string userId, CancellationToken cancellationToken = default);
    Task<ActivityDto> UpdateAsync(int id, UpdateActivityDto dto, string userId, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
