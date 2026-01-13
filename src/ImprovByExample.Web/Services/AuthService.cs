using ImprovByExample.Application.Common.Models.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace ImprovByExample.Web.Services;

/// <summary>
/// Implementation of authentication service
/// </summary>
public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public AuthService(HttpClient httpClient, ILogger<AuthService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<UserDto?> RegisterAsync(RegisterDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", dto, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogWarning("Registration failed: {StatusCode}, {Error}", response.StatusCode, errorContent);
                return null;
            }

            var user = await response.Content.ReadFromJsonAsync<UserDto>(_jsonOptions, cancellationToken);
            _logger.LogInformation("User registered successfully: {Email}", user?.Email);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration for {Email}", dto.Email);
            return null;
        }
    }

    public async Task<UserDto?> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", dto, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogWarning("Login failed: {StatusCode}, {Error}", response.StatusCode, errorContent);
                return null;
            }

            var user = await response.Content.ReadFromJsonAsync<UserDto>(_jsonOptions, cancellationToken);
            _logger.LogInformation("User logged in successfully: {Email}", user?.Email);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for {Email}", dto.Email);
            return null;
        }
    }

    public async Task<bool> LogoutAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsync("api/auth/logout", null, cancellationToken);
            
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Logout failed: {StatusCode}", response.StatusCode);
                return false;
            }

            _logger.LogInformation("User logged out successfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during logout");
            return false;
        }
    }

    public async Task<UserDto?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync("api/auth/user", cancellationToken);
            
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                _logger.LogDebug("No authenticated user");
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Get current user failed: {StatusCode}", response.StatusCode);
                return null;
            }

            var user = await response.Content.ReadFromJsonAsync<UserDto>(_jsonOptions, cancellationToken);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting current user");
            return null;
        }
    }
}
