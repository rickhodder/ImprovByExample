namespace ImprovByExample.Application.Common.Models.DTOs;

/// <summary>
/// DTO for user information
/// </summary>
public class UserDto
{
    /// <summary>
    /// User's unique identifier
    /// </summary>
    public required string Id { get; set; }
    
    /// <summary>
    /// User's email address
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// User's first name
    /// </summary>
    public string? FirstName { get; set; }
    
    /// <summary>
    /// User's last name
    /// </summary>
    public string? LastName { get; set; }
    
    /// <summary>
    /// User's roles
    /// </summary>
    public required IList<string> Roles { get; set; }
}
