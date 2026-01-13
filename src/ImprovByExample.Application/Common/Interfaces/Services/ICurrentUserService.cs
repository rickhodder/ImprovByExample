namespace ImprovByExample.Application.Common.Interfaces.Services;

/// <summary>
/// Service for accessing current user information
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the current user's ID
    /// </summary>
    string? UserId { get; }

    /// <summary>
    /// Gets the current user's email
    /// </summary>
    string? UserEmail { get; }

    /// <summary>
    /// Checks if the current user is authenticated
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Checks if the current user is an admin
    /// </summary>
    bool IsAdmin { get; }
}
