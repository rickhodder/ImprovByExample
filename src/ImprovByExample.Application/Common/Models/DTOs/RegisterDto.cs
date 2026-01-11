namespace ImprovByExample.Application.Common.Models.DTOs;

/// <summary>
/// DTO for user registration
/// </summary>
public class RegisterDto
{
    /// <summary>
    /// User's email address (used as username)
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// User's password
    /// </summary>
    public required string Password { get; set; }
    
    /// <summary>
    /// Password confirmation (must match Password)
    /// </summary>
    public required string ConfirmPassword { get; set; }
    
    /// <summary>
    /// User's first name (optional)
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// User's last name (optional)
    /// </summary>
    public string? LastName { get; set; }
}
