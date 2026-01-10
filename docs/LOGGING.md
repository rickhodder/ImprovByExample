# Structured Logging with Serilog

## Overview

This project uses **Serilog** for structured logging throughout all application layers. Structured logging provides rich, searchable log data that includes contextual information, making it easier to debug issues and monitor application behavior.

## Configuration

### Serilog Setup

Serilog is configured in both the API and Web applications using the following components:

**Packages Used:**
- `Serilog.AspNetCore` - Core Serilog integration with ASP.NET Core
- `Serilog.Sinks.Console` - Console output
- `Serilog.Sinks.File` - File output with rolling intervals
- `Serilog.Enrichers.Environment` - Adds environment name
- `Serilog.Enrichers.Thread` - Adds thread ID
- `Serilog.Enrichers.Process` - Adds process ID

### Log Sinks

**Console Sink:**
- Outputs logs to the console with a readable format
- Template: `[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}`

**File Sink:**
- Writes logs to rolling daily files
- API logs: `logs/api-YYYYMMDD.log`
- Web logs: `logs/web-YYYYMMDD.log`
- Retains 30 days of logs by default
- Template: `{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}`

### Log Enrichers

The following enrichers add contextual information to each log entry:

- **Environment Name**: Development, Staging, Production
- **Machine Name**: Host machine name
- **Thread ID**: Thread executing the code
- **Process ID**: Process ID of the application
- **From Log Context**: Additional properties added per-request

### Log Levels

Log levels are configured per namespace in `appsettings.json`:

**Production (`appsettings.json`):**
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.AspNetCore": "Warning",
        "Microsoft.EntityFrameworkCore": "Warning",
        "System": "Warning"
      }
    }
  }
}
```

**Development (`appsettings.Development.json`):**
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Debug",
      "Override": {
        "Microsoft": "Information",
        "Microsoft.AspNetCore": "Warning",
        "Microsoft.EntityFrameworkCore": "Information",
        "System": "Information"
      }
    }
  }
}
```

## Usage in Code

### Services

Services receive `ILogger<T>` via dependency injection:

```csharp
public class ActivityService : IActivityService
{
    private readonly ILogger<ActivityService> _logger;

    public ActivityService(ILogger<ActivityService> logger)
    {
        _logger = logger;
    }

    public async Task<ActivityDto?> GetActivityByIdAsync(int id)
    {
        _logger.LogDebug("Getting activity by ID: {ActivityId}", id);
        
        var activity = await _repository.GetByIdAsync(id);
        
        if (activity == null)
        {
            _logger.LogWarning("Activity with ID {ActivityId} not found", id);
            return null;
        }
        
        _logger.LogInformation("Retrieved activity: {ActivityId} - {ActivityName}", 
            id, activity.Name);
        
        return MapToDto(activity);
    }
}
```

### Controllers

Controllers also receive `ILogger<T>` via dependency injection:

```csharp
[ApiController]
[Route("api/[controller]")]
public class ActivitiesController : ControllerBase
{
    private readonly ILogger<ActivitiesController> _logger;

    public ActivitiesController(ILogger<ActivitiesController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityDto dto)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid data provided for CreateActivity: {Errors}", 
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            return BadRequest(validationResult.Errors);
        }
        
        // ... rest of method
    }
}
```

## Request Logging

HTTP requests are automatically logged with the following information:
- HTTP method and path
- Status code
- Response time in milliseconds
- Request host, scheme, and user agent

Example log entry:
```
HTTP GET /api/activities responded 200 in 45.2367 ms
```

## Log File Rotation

Log files automatically rotate daily:
- Each day creates a new file: `api-20260110.log`, `api-20260111.log`, etc.
- Files older than 30 days are automatically deleted
- No manual cleanup required

## Best Practices

### Log Levels

Use appropriate log levels for different scenarios:

- **Debug**: Detailed information for diagnosing issues (development only)
  - Example: "Getting activity by ID: 123"
  
- **Information**: General informational messages about application flow
  - Example: "Created activity: 1 - Zip Zap Zop"
  
- **Warning**: Something unexpected happened, but the application continues
  - Example: "Activity with ID 999 not found"
  
- **Error**: An error occurred that prevented an operation from completing
  - Example: "Failed to save activity: Database connection timeout"
  
- **Critical**: A critical failure requiring immediate attention
  - Example: "Database connection pool exhausted"

### Structured Properties

Always use structured properties (placeholders) instead of string interpolation:

✅ **Good:**
```csharp
_logger.LogInformation("Created activity: {ActivityId} - {ActivityName}", 
    activity.Id, activity.Name);
```

❌ **Bad:**
```csharp
_logger.LogInformation($"Created activity: {activity.Id} - {activity.Name}");
```

Structured properties enable powerful log querying and filtering.

### Avoid Logging Sensitive Data

Never log sensitive information:
- Passwords
- API keys
- Personal identification numbers
- Credit card numbers
- Email addresses (in most contexts)

## Testing Logging

The project includes unit tests to verify logging behavior using Moq:

```csharp
[Fact]
public async Task GetActivityByIdAsync_LogsWarning_WhenActivityNotFound()
{
    // Arrange
    var activityId = 999;
    _mockRepository
        .Setup(r => r.GetByIdAsync(activityId))
        .ReturnsAsync((Activity?)null);

    // Act
    var result = await _service.GetActivityByIdAsync(activityId);

    // Assert
    _mockLogger.Verify(
        x => x.Log(
            LogLevel.Warning,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => 
                v.ToString()!.Contains($"Activity with ID {activityId} not found")),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
        Times.Once);
}
```

## Log File Location

Log files are created in the following locations:

- **API**: `src/ImprovByExample.Api/logs/`
- **Web**: `src/ImprovByExample.Web/logs/`

These directories are excluded from git via `.gitignore`.

## Future Enhancements (Optional)

Consider these enhancements for production deployments:

1. **Seq Integration**: Add Serilog.Sinks.Seq for centralized log aggregation
2. **Application Insights**: Add Serilog.Sinks.ApplicationInsights for Azure monitoring
3. **Elastic Stack**: Add Serilog.Sinks.Elasticsearch for ELK stack integration
4. **Alerts**: Configure alerts for Error and Critical level logs
5. **Performance Monitoring**: Add custom metrics and performance counters

## References

- [Serilog Documentation](https://serilog.net/)
- [Serilog Best Practices](https://github.com/serilog/serilog/wiki/Structured-Data)
- [ASP.NET Core Logging](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/)
