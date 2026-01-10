using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ImprovByExample.Application.Common.Interfaces.Services;
using ImprovByExample.Application.Common.Models.DTOs;
using System.Security.Claims;

namespace ImprovByExample.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly IActivityService _activityService;
    private readonly ILogger<ActivitiesController> _logger;

    public ActivitiesController(IActivityService activityService, ILogger<ActivitiesController> logger)
    {
        _activityService = activityService;
        _logger = logger;
    }

    /// <summary>
    /// Get all activities with optional filtering and pagination
    /// </summary>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetActivities([FromQuery] ActivityFilterDto filter)
    {
        var result = await _activityService.SearchAsync(filter);
        return Ok(result);
    }

    /// <summary>
    /// Get a specific activity by ID
    /// </summary>
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetActivity(int id)
    {
        var activity = await _activityService.GetByIdAsync(id);
        if (activity == null)
        {
            return NotFound();
        }
        return Ok(activity);
    }

    /// <summary>
    /// Create a new activity (Admin only)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var activity = await _activityService.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(GetActivity), new { id = activity.Id }, activity);
    }

    /// <summary>
    /// Update an existing activity (Admin only)
    /// </summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateActivity(int id, [FromBody] UpdateActivityDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        try
        {
            var activity = await _activityService.UpdateAsync(id, dto, userId);
            return Ok(activity);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Delete an activity (Admin only)
    /// </summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteActivity(int id)
    {
        try
        {
            await _activityService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
