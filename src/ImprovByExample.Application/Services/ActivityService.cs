using ImprovByExample.Application.Common.Interfaces.Repositories;
using ImprovByExample.Application.Common.Interfaces.Services;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Common.Models.Responses;
using ImprovByExample.Application.Specifications;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Application.Services;

public class ActivityService : IActivityService
{
    private readonly IRepository<ImprovActivity> _activityRepository;
    private readonly IReadRepository<ImprovActivity> _readRepository;

    public ActivityService(
        IRepository<ImprovActivity> activityRepository,
        IReadRepository<ImprovActivity> readRepository)
    {
        _activityRepository = activityRepository;
        _readRepository = readRepository;
    }

    public async Task<ActivityDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var activity = await _activityRepository.GetByIdAsync(id, cancellationToken);
        return activity == null ? null : MapToDto(activity);
    }

    public async Task<PagedResult<ActivityDto>> SearchAsync(ActivityFilterDto filter, CancellationToken cancellationToken = default)
    {
        var activities = await _readRepository.ListAsync(cancellationToken);
        var totalCount = await _readRepository.CountAsync(cancellationToken);

        return new PagedResult<ActivityDto>
        {
            Items = activities.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            PageIndex = filter.PageIndex,
            PageSize = filter.PageSize
        };
    }

    public async Task<ActivityDto> CreateAsync(CreateActivityDto dto, string userId, CancellationToken cancellationToken = default)
    {
        var activity = new ImprovActivity
        {
            Name = dto.Name,
            ActivityTypeId = dto.ActivityTypeId,
            ActivitySourceId = dto.ActivitySourceId,
            Description = dto.Description,
            Rules = dto.Rules,
            Script = dto.Script,
            Category = dto.Category,
            DifficultyId = dto.DifficultyId,
            MinPlayers = dto.MinPlayers,
            MaxPlayers = dto.MaxPlayers,
            DurationMinutes = dto.DurationMinutes,
            Tags = dto.Tags,
            CreatedById = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _activityRepository.AddAsync(activity, cancellationToken);
        return MapToDto(created);
    }

    public async Task<ActivityDto> UpdateAsync(int id, UpdateActivityDto dto, string userId, CancellationToken cancellationToken = default)
    {
        var activity = await _activityRepository.GetByIdAsync(id, cancellationToken);
        if (activity == null)
        {
            throw new KeyNotFoundException($"Activity with ID {id} not found");
        }

        activity.Name = dto.Name;
        activity.ActivityTypeId = dto.ActivityTypeId;
        activity.ActivitySourceId = dto.ActivitySourceId;
        activity.Description = dto.Description;
        activity.Rules = dto.Rules;
        activity.Script = dto.Script;
        activity.Category = dto.Category;
        activity.DifficultyId = dto.DifficultyId;
        activity.MinPlayers = dto.MinPlayers;
        activity.MaxPlayers = dto.MaxPlayers;
        activity.DurationMinutes = dto.DurationMinutes;
        activity.Tags = dto.Tags;
        activity.UpdatedById = userId;
        activity.UpdatedAt = DateTime.UtcNow;

        await _activityRepository.UpdateAsync(activity, cancellationToken);
        return MapToDto(activity);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var activity = await _activityRepository.GetByIdAsync(id, cancellationToken);
        if (activity == null)
        {
            throw new KeyNotFoundException($"Activity with ID {id} not found");
        }

        await _activityRepository.DeleteAsync(activity, cancellationToken);
    }

    private static ActivityDto MapToDto(ImprovActivity activity)
    {
        return new ActivityDto
        {
            Id = activity.Id,
            Name = activity.Name,
            ActivityType = activity.ActivityType?.Name ?? string.Empty,
            ActivitySource = activity.ActivitySource?.Name,
            Description = activity.Description,
            Rules = activity.Rules,
            Script = activity.Script,
            Category = activity.Category,
            Difficulty = activity.Difficulty?.Name,
            MinPlayers = activity.MinPlayers,
            MaxPlayers = activity.MaxPlayers,
            DurationMinutes = activity.DurationMinutes,
            Tags = activity.Tags,
            CreatedAt = activity.CreatedAt,
            UpdatedAt = activity.UpdatedAt
        };
    }
}
