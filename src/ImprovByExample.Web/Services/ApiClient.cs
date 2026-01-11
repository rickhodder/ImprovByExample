using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Common.Models.Responses;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ImprovByExample.Web.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ApiClient> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiClient(HttpClient httpClient, ILogger<ApiClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    // Activities
    public async Task<PagedResult<ActivityDto>?> GetActivitiesAsync(ActivityFilterDto filter, CancellationToken cancellationToken = default)
    {
        try
        {
            var queryString = BuildQueryString(filter);
            var response = await _httpClient.GetAsync($"api/activities?{queryString}", cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PagedResult<ActivityDto>>(_jsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching activities with filter: {@Filter}", filter);
            return null;
        }
    }

    public async Task<ActivityDto?> GetActivityByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/activities/{id}", cancellationToken);
            
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ActivityDto>(_jsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching activity with ID: {ActivityId}", id);
            return null;
        }
    }

    public async Task<ActivityDto?> CreateActivityAsync(CreateActivityDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/activities", dto, _jsonOptions, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ActivityDto>(_jsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating activity: {@Activity}", dto);
            throw;
        }
    }

    public async Task<ActivityDto?> UpdateActivityAsync(int id, UpdateActivityDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/activities/{id}", dto, _jsonOptions, cancellationToken);
            
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ActivityDto>(_jsonOptions, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating activity with ID: {ActivityId}", id);
            throw;
        }
    }

    public async Task<bool> DeleteActivityAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/activities/{id}", cancellationToken);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting activity with ID: {ActivityId}", id);
            return false;
        }
    }

    private static string BuildQueryString(ActivityFilterDto filter)
    {
        var parameters = new List<string>();

        if (!string.IsNullOrEmpty(filter.SearchTerm))
            parameters.Add($"searchTerm={Uri.EscapeDataString(filter.SearchTerm)}");
        
        if (filter.ActivityTypeId.HasValue)
            parameters.Add($"activityTypeId={filter.ActivityTypeId.Value}");
        
        if (filter.ActivitySourceId.HasValue)
            parameters.Add($"activitySourceId={filter.ActivitySourceId.Value}");
        
        if (filter.DifficultyId.HasValue)
            parameters.Add($"difficultyId={filter.DifficultyId.Value}");
        
        if (filter.MinPlayers.HasValue)
            parameters.Add($"minPlayers={filter.MinPlayers.Value}");
        
        if (filter.MaxPlayers.HasValue)
            parameters.Add($"maxPlayers={filter.MaxPlayers.Value}");
        
        parameters.Add($"pageNumber={filter.PageNumber}");
        parameters.Add($"pageSize={filter.PageSize}");

        return string.Join("&", parameters);
    }
}
