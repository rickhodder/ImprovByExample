using ImprovByExample.Application.Common.Interfaces.Repositories;
using ImprovByExample.Application.Common.Interfaces.Services;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Common.Models.Responses;
using ImprovByExample.Application.Specifications;
using ImprovByExample.Domain.Entities;

namespace ImprovByExample.Application.Services;

public class ActivityService : IActivityService
{
    private readonly IReadRepository<ImprovActivity> _readRepository;
    private readonly IRepository<ImprovActivity> _repository;

    public ActivityService(
        IReadRepository<ImprovActivity> readRepository,
        IRepository<ImprovActivity> repository)
    {
        _readRepository = readRepository;
        _repository = repository;
    }

    public async Task<PagedResult<ActivityDto>> GetActivitiesAsync(
        ActivityFilterDto filter, 
        CancellationToken cancellationToken = default)
    {
        var spec = new ActivitiesFilterSpec(filter);
        var activities = await _readRepository.ListAsync(spec, cancellationToken);
        
        // Get total count for pagination (without paging applied)
        var countFilter = new ActivityFilterDto
        {
            SearchTerm = filter.SearchTerm,
            ActivityTypeId = filter.ActivityTypeId,
            ActivitySourceId = filter.ActivitySourceId,
            DifficultyId = filter.DifficultyId,
            MinPlayers = filter.MinPlayers,
            MaxPlayers = filter.MaxPlayers,
            PageSize = 0 // Disable paging for count
        };
        var countSpec = new ActivitiesFilterSpec(countFilter);
        var totalCount = await _readRepository.CountAsync(countSpec, cancellationToken);

        return new PagedResult<ActivityDto>
        {
            Items = activities.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    public async Task<ActivityDto?> GetActivityByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var spec = new ActivityByIdSpec(id);
        var activity = await _readRepository.FirstOrDefaultAsync(spec, cancellationToken);
        
        return activity == null ? null : MapToDto(activity);
    }

    public async Task<ActivityDto> CreateActivityAsync(
        CreateActivityDto dto, 
        string createdById, 
        CancellationToken cancellationToken = default)
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
            CreatedById = createdById,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var created = await _repository.AddAsync(activity, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        // Reload with includes
        return (await GetActivityByIdAsync(created.Id, cancellationToken))!;
    }

    public async Task<ActivityDto?> UpdateActivityAsync(
        UpdateActivityDto dto, 
        string updatedById, 
        CancellationToken cancellationToken = default)
    {
        var activity = await _repository.GetByIdAsync(dto.Id, cancellationToken);
        if (activity == null)
            return null;

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
        activity.UpdatedById = updatedById;
        activity.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(activity, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        // Reload with includes
        return await GetActivityByIdAsync(activity.Id, cancellationToken);
    }

    public async Task<bool> DeleteActivityAsync(int id, CancellationToken cancellationToken = default)
    {
        var activity = await _repository.GetByIdAsync(id, cancellationToken);
        if (activity == null)
            return false;

        await _repository.DeleteAsync(activity, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }

    private static ActivityDto MapToDto(ImprovActivity activity)
    {
        return new ActivityDto
        {
            Id = activity.Id,
            Name = activity.Name,
            ActivityTypeId = activity.ActivityTypeId,
            ActivityTypeName = activity.ActivityType?.Name ?? string.Empty,
            ActivitySourceId = activity.ActivitySourceId,
            ActivitySourceName = activity.ActivitySource?.Name,
            Description = activity.Description,
            Rules = activity.Rules,
            Script = activity.Script,
            Category = activity.Category,
            DifficultyId = activity.DifficultyId,
            DifficultyName = activity.Difficulty?.Name,
            MinPlayers = activity.MinPlayers,
            MaxPlayers = activity.MaxPlayers,
            DurationMinutes = activity.DurationMinutes,
            Tags = activity.Tags,
            CreatedAt = activity.CreatedAt,
            UpdatedAt = activity.UpdatedAt
        };
    }
}
