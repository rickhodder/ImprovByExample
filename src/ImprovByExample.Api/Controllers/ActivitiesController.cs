using FluentValidation;
using ImprovByExample.Application.Common.Interfaces.Services;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Common.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImprovByExample.Api.Controllers;

/// <summary>
/// Manages improv activities including games, techniques, warmups, and exercises
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _activityService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<CreateActivityDto> _createValidator;
    private readonly IValidator<UpdateActivityDto> _updateValidator;
    private readonly IValidator<ActivityFilterDto> _filterValidator;
    private readonly ILogger<ActivitiesController> _logger;

    public ActivitiesController(
        IActivityService activityService,
        ICurrentUserService currentUserService,
        IValidator<CreateActivityDto> createValidator,
        IValidator<UpdateActivityDto> updateValidator,
        IValidator<ActivityFilterDto> filterValidator,
        ILogger<ActivitiesController> logger)
    {
        _activityService = activityService;
        _currentUserService = currentUserService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _filterValidator = filterValidator;
        _logger = logger;
    }

    /// <summary>
    /// Gets a paginated list of activities with optional filtering
    /// </summary>
    /// <param name="filter">Filter criteria for activities</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Paginated list of activities</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PagedResult<ActivityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetActivities(
        [FromQuery] ActivityFilterDto filter,
        CancellationToken cancellationToken)
    {
        var validationResult = await _filterValidator.ValidateAsync(filter, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid filter provided for GetActivities: {Errors}", 
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            return BadRequest(validationResult.Errors);
        }

        var result = await _activityService.GetActivitiesAsync(filter, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a specific activity by ID
    /// </summary>
    /// <param name="id">Activity ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Activity details</returns>
    [HttpGet("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetActivity(int id, CancellationToken cancellationToken)
    {
        var activity = await _activityService.GetActivityByIdAsync(id, cancellationToken);
        
        if (activity == null)
        {
            return NotFound(new { message = $"Activity with ID {id} not found." });
        }

        return Ok(activity);
    }

    /// <summary>
    /// Creates a new activity (Admin only)
    /// </summary>
    /// <param name="dto">Activity creation data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Created activity</returns>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateActivity(
        [FromBody] CreateActivityDto dto,
        CancellationToken cancellationToken)
    {
        var validationResult = await _createValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid data provided for CreateActivity: {Errors}", 
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            return BadRequest(validationResult.Errors);
        }

        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("CreateActivity called but userId is null");
            return Unauthorized(new { message = "User authentication required." });
        }

        var created = await _activityService.CreateActivityAsync(dto, userId, cancellationToken);
        
        return CreatedAtAction(
            nameof(GetActivity),
            new { id = created.Id },
            created);
    }

    /// <summary>
    /// Updates an existing activity (Admin only)
    /// </summary>
    /// <param name="id">Activity ID</param>
    /// <param name="dto">Activity update data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Updated activity</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateActivity(
        int id,
        [FromBody] UpdateActivityDto dto,
        CancellationToken cancellationToken)
    {
        if (id != dto.Id)
        {
            _logger.LogWarning("UpdateActivity: ID mismatch - URL: {UrlId}, Body: {BodyId}", id, dto.Id);
            return BadRequest(new { message = "ID in URL does not match ID in body." });
        }

        var validationResult = await _updateValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid data provided for UpdateActivity {ActivityId}: {Errors}", id,
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            return BadRequest(validationResult.Errors);
        }

        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("UpdateActivity called but userId is null");
            return Unauthorized(new { message = "User authentication required." });
        }

        var updated = await _activityService.UpdateActivityAsync(dto, userId, cancellationToken);
        
        if (updated == null)
        {
            return NotFound(new { message = $"Activity with ID {id} not found." });
        }

        return Ok(updated);
    }

    /// <summary>
    /// Deletes an activity (Admin only)
    /// </summary>
    /// <param name="id">Activity ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>No content on success</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteActivity(int id, CancellationToken cancellationToken)
    {
        var result = await _activityService.DeleteActivityAsync(id, cancellationToken);
        
        if (!result)
        {
            return NotFound(new { message = $"Activity with ID {id} not found." });
        }

        return NoContent();
    }
}
