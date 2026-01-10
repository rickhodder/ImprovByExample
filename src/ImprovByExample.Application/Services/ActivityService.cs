using ImprovByExample.Application.Common.Interfaces.Repositories;
using ImprovByExample.Application.Common.Interfaces.Services;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Common.Models.Responses;
using ImprovByExample.Application.Specifications;
using ImprovByExample.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ImprovByExample.Application.Services;

public class ActivityService : IActivityService
{
    private readonly IReadRepository<ImprovActivity> _readRepository;
    private readonly IRepository<ImprovActivity> _repository;
    private readonly ILogger<ActivityService> _logger;

    public ActivityService(
        IReadRepository<ImprovActivity> readRepository,
        IRepository<ImprovActivity> repository,
        ILogger<ActivityService> logger)
    {
        _readRepository = readRepository;
        _repository = repository;
        _logger = logger;
    }

    public async Task<PagedResult<ActivityDto>> GetActivitiesAsync(
        ActivityFilterDto filter, 
        CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Getting activities with filter: SearchTerm={SearchTerm}, ActivityTypeId={ActivityTypeId}, PageNumber={PageNumber}, PageSize={PageSize}",
            filter.SearchTerm, filter.ActivityTypeId, filter.PageNumber, filter.PageSize);

        var spec = new ActivitiesFilterSpec(filter);
        var activities = await _readRepository.ListAsync(spec, cancellationToken);
        
        // Get total count for pagination using a separate count specification
        var countSpec = new ActivitiesCountSpec(filter);
        var totalCount = await _readRepository.CountAsync(countSpec, cancellationToken);

        _logger.LogInformation("Retrieved {Count} activities (Total: {TotalCount})", activities.Count, totalCount);

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
        _logger.LogDebug("Getting activity by ID: {ActivityId}", id);
        
        var spec = new ActivityByIdSpec(id);
        var activity = await _readRepository.FirstOrDefaultAsync(spec, cancellationToken);
        
        if (activity == null)
        {
            _logger.LogWarning("Activity with ID {ActivityId} not found", id);
        }
        else
        {
            _logger.LogInformation("Retrieved activity: {ActivityId} - {ActivityName}", id, activity.Name);
        }
        
        return activity == null ? null : MapToDto(activity);
    }

    public async Task<ActivityDto> CreateActivityAsync(
        CreateActivityDto dto, 
        string createdById, 
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new activity: {ActivityName} by user {UserId}", dto.Name, createdById);
        
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

        _logger.LogInformation("Successfully created activity: {ActivityId} - {ActivityName}", created.Id, created.Name);

        // Reload with includes
        return (await GetActivityByIdAsync(created.Id, cancellationToken))!;
    }

    public async Task<ActivityDto?> UpdateActivityAsync(
        UpdateActivityDto dto, 
        string updatedById, 
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating activity: {ActivityId} by user {UserId}", dto.Id, updatedById);
        
        var activity = await _repository.GetByIdAsync(dto.Id, cancellationToken);
        if (activity == null)
        {
            _logger.LogWarning("Cannot update activity {ActivityId} - not found", dto.Id);
            return null;
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
        activity.UpdatedById = updatedById;
        activity.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(activity, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully updated activity: {ActivityId} - {ActivityName}", activity.Id, activity.Name);

        // Reload with includes
        return await GetActivityByIdAsync(activity.Id, cancellationToken);
    }

    public async Task<bool> DeleteActivityAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to delete activity: {ActivityId}", id);
        
        var activity = await _repository.GetByIdAsync(id, cancellationToken);
        if (activity == null)
        {
            _logger.LogWarning("Cannot delete activity {ActivityId} - not found", id);
            return false;
        }

        await _repository.DeleteAsync(activity, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully deleted activity: {ActivityId} - {ActivityName}", id, activity.Name);

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
