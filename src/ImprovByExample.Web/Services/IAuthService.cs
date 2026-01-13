using ImprovByExample.Application.Common.Models.DTOs;

namespace ImprovByExample.Web.Services;

/// <summary>
/// Service for handling user authentication
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Register a new user account
    /// </summary>
    /// <param name="dto">Registration data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User information if successful, null otherwise</returns>
    Task<UserDto?> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Login with email and password
    /// </summary>
    /// <param name="dto">Login credentials</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User information if successful, null otherwise</returns>
    Task<UserDto?> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Logout the current user
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if successful</returns>
    Task<bool> LogoutAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get current authenticated user information
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>User information if authenticated, null otherwise</returns>
    Task<UserDto?> GetCurrentUserAsync(CancellationToken cancellationToken = default);
}
