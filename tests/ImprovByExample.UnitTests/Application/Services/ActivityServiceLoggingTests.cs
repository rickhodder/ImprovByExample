using FluentAssertions;
using ImprovByExample.Application.Common.Interfaces.Repositories;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Services;
using ImprovByExample.Application.Specifications;
using ImprovByExample.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace ImprovByExample.UnitTests.Application.Services;

public class ActivityServiceLoggingTests
{
    private readonly Mock<IReadRepository<ImprovActivity>> _mockReadRepository;
    private readonly Mock<IRepository<ImprovActivity>> _mockRepository;
    private readonly Mock<ILogger<ActivityService>> _mockLogger;
    private readonly ActivityService _service;

    public ActivityServiceLoggingTests()
    {
        _mockReadRepository = new Mock<IReadRepository<ImprovActivity>>();
        _mockRepository = new Mock<IRepository<ImprovActivity>>();
        _mockLogger = new Mock<ILogger<ActivityService>>();
        _service = new ActivityService(_mockReadRepository.Object, _mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetActivityByIdAsync_LogsDebugMessage_WhenActivityIsRequested()
    {
        // Arrange
        var activityId = 1;
        var activity = new ImprovActivity
        {
            Id = activityId,
            Name = "Test Activity",
            Description = "Test Description",
            Rules = "Test Rules",
            Category = "Test Category",
            Tags = Array.Empty<string>(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedById = "user123"
        };

        _mockReadRepository
            .Setup(r => r.FirstOrDefaultAsync(It.IsAny<ActivityByIdSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(activity);

        // Act
        var result = await _service.GetActivityByIdAsync(activityId);

        // Assert
        result.Should().NotBeNull();
        
        // Verify that debug log was called with the activity ID
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Debug,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Getting activity by ID: {activityId}")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task GetActivityByIdAsync_LogsWarning_WhenActivityNotFound()
    {
        // Arrange
        var activityId = 999;

        _mockReadRepository
            .Setup(r => r.FirstOrDefaultAsync(It.IsAny<ActivityByIdSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ImprovActivity?)null);

        // Act
        var result = await _service.GetActivityByIdAsync(activityId);

        // Assert
        result.Should().BeNull();
        
        // Verify that warning log was called
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Activity with ID {activityId} not found")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateActivityAsync_LogsInformation_WhenActivityIsCreated()
    {
        // Arrange
        var dto = new CreateActivityDto
        {
            Name = "New Activity",
            ActivityTypeId = 1,
            Description = "Description",
            Rules = "Rules",
            Category = "Category",
            Tags = Array.Empty<string>()
        };
        var userId = "user123";
        var createdActivity = new ImprovActivity
        {
            Id = 1,
            Name = dto.Name,
            Description = dto.Description,
            Rules = dto.Rules,
            Category = dto.Category,
            Tags = dto.Tags,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedById = userId
        };

        _mockRepository
            .Setup(r => r.AddAsync(It.IsAny<ImprovActivity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdActivity);

        _mockRepository
            .Setup(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _mockReadRepository
            .Setup(r => r.FirstOrDefaultAsync(It.IsAny<ActivityByIdSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdActivity);

        // Act
        var result = await _service.CreateActivityAsync(dto, userId);

        // Assert
        result.Should().NotBeNull();
        
        // Verify that information log was called
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("Creating new activity")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task DeleteActivityAsync_LogsWarning_WhenActivityNotFound()
    {
        // Arrange
        var activityId = 999;

        _mockRepository
            .Setup(r => r.GetByIdAsync(activityId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ImprovActivity?)null);

        // Act
        var result = await _service.DeleteActivityAsync(activityId);

        // Assert
        result.Should().BeFalse();
        
        // Verify that warning log was called
        _mockLogger.Verify(
            x => x.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains($"Cannot delete activity {activityId} - not found")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
