namespace ImprovByExample.Application.Common.Models.DTOs;

/// <summary>
/// DTO for user login
/// </summary>
public class LoginDto
{
    /// <summary>
    /// User's email address
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// User's password
    /// </summary>
    public required string Password { get; set; }
    
    /// <summary>
    /// Whether to persist the login cookie beyond the session
    /// </summary>
    public bool RememberMe { get; set; }
}
