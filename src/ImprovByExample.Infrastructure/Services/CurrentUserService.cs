using ImprovByExample.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ImprovByExample.Infrastructure.Services;

/// <summary>
/// Implementation of current user service
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Gets the current user's ID
    /// </summary>
    public string? UserId =>
        _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    /// <summary>
    /// Gets the current user's email
    /// </summary>
    public string? UserEmail =>
        _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ??
        _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

    /// <summary>
    /// Checks if the current user is authenticated
    /// </summary>
    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    /// <summary>
    /// Checks if the current user is an admin
    /// </summary>
    public bool IsAdmin =>
        _httpContextAccessor.HttpContext?.User?.IsInRole("Admin") ?? false;
}
