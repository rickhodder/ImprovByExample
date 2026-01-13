using FluentAssertions;
using ImprovByExample.Application.Common.Interfaces.Repositories;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Services;
using ImprovByExample.Application.Specifications;
using ImprovByExample.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace ImprovByExample.UnitTests.Application.Services;

public class ActivityServiceTests
{
    private readonly Mock<IReadRepository<ImprovActivity>> _readRepositoryMock;
    private readonly Mock<IRepository<ImprovActivity>> _repositoryMock;
    private readonly Mock<ILogger<ActivityService>> _loggerMock;
    private readonly ActivityService _service;

    public ActivityServiceTests()
    {
        _readRepositoryMock = new Mock<IReadRepository<ImprovActivity>>();
        _repositoryMock = new Mock<IRepository<ImprovActivity>>();
        _loggerMock = new Mock<ILogger<ActivityService>>();
        _service = new ActivityService(_readRepositoryMock.Object, _repositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetActivitiesAsync_ReturnsPagedResult_WithActivities()
    {
        // Arrange
        var filter = new ActivityFilterDto { PageNumber = 1, PageSize = 10 };
        var activities = new List<ImprovActivity>
        {
            CreateTestActivity(1, "Zip Zap Zop"),
            CreateTestActivity(2, "Yes And")
        };

        _readRepositoryMock
            .Setup(r => r.ListAsync(It.IsAny<ActivitiesFilterSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(activities);

        _readRepositoryMock
            .Setup(r => r.CountAsync(It.IsAny<ActivitiesCountSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(2);

        // Act
        var result = await _service.GetActivitiesAsync(filter);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(2);
        result.PageNumber.Should().Be(1);
        result.PageSize.Should().Be(10);
        result.Items[0].Name.Should().Be("Zip Zap Zop");
        result.Items[1].Name.Should().Be("Yes And");
    }

    [Fact]
    public async Task GetActivitiesAsync_ReturnsEmptyResult_WhenNoActivitiesFound()
    {
        // Arrange
        var filter = new ActivityFilterDto { PageNumber = 1, PageSize = 10 };
        
        _readRepositoryMock
            .Setup(r => r.ListAsync(It.IsAny<ActivitiesFilterSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<ImprovActivity>());

        _readRepositoryMock
            .Setup(r => r.CountAsync(It.IsAny<ActivitiesCountSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        var result = await _service.GetActivitiesAsync(filter);

        // Assert
        result.Should().NotBeNull();
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
    }

    [Fact]
    public async Task GetActivityByIdAsync_ReturnsActivity_WhenExists()
    {
        // Arrange
        var activity = CreateTestActivity(1, "Zip Zap Zop");
        
        _readRepositoryMock
            .Setup(r => r.FirstOrDefaultAsync(It.IsAny<ActivityByIdSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(activity);

        // Act
        var result = await _service.GetActivityByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.Name.Should().Be("Zip Zap Zop");
    }

    [Fact]
    public async Task GetActivityByIdAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        _readRepositoryMock
            .Setup(r => r.FirstOrDefaultAsync(It.IsAny<ActivityByIdSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ImprovActivity?)null);

        // Act
        var result = await _service.GetActivityByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateActivityAsync_CreatesAndReturnsActivity()
    {
        // Arrange
        var dto = new CreateActivityDto
        {
            Name = "New Activity",
            ActivityTypeId = 1,
            Description = "Test description",
            Rules = "Test rules",
            Category = "Test"
        };

        var createdActivity = CreateTestActivity(1, "New Activity");
        
        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<ImprovActivity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdActivity);

        _readRepositoryMock
            .Setup(r => r.FirstOrDefaultAsync(It.IsAny<ActivityByIdSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdActivity);

        // Act
        var result = await _service.CreateActivityAsync(dto, "user123");

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("New Activity");
        
        _repositoryMock.Verify(r => r.AddAsync(It.Is<ImprovActivity>(a => a.Name == "New Activity"), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateActivityAsync_UpdatesAndReturnsActivity_WhenExists()
    {
        // Arrange
        var dto = new UpdateActivityDto
        {
            Id = 1,
            Name = "Updated Activity",
            ActivityTypeId = 1,
            Description = "Updated description",
            Rules = "Updated rules",
            Category = "Test"
        };

        var existingActivity = CreateTestActivity(1, "Original Name");
        var updatedActivity = CreateTestActivity(1, "Updated Activity");

        _repositoryMock
            .Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingActivity);

        _readRepositoryMock
            .Setup(r => r.FirstOrDefaultAsync(It.IsAny<ActivityByIdSpec>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(updatedActivity);

        // Act
        var result = await _service.UpdateActivityAsync(dto, "user123");

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be("Updated Activity");
        
        _repositoryMock.Verify(r => r.UpdateAsync(It.Is<ImprovActivity>(a => a.Name == "Updated Activity"), It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task UpdateActivityAsync_ReturnsNull_WhenActivityNotFound()
    {
        // Arrange
        var dto = new UpdateActivityDto
        {
            Id = 999,
            Name = "Updated Activity",
            ActivityTypeId = 1,
            Description = "Updated description",
            Rules = "Updated rules",
            Category = "Test"
        };

        _repositoryMock
            .Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ImprovActivity?)null);

        // Act
        var result = await _service.UpdateActivityAsync(dto, "user123");

        // Assert
        result.Should().BeNull();
        
        _repositoryMock.Verify(r => r.UpdateAsync(It.IsAny<ImprovActivity>(), It.IsAny<CancellationToken>()), Times.Never);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task DeleteActivityAsync_DeletesAndReturnsTrue_WhenExists()
    {
        // Arrange
        var activity = CreateTestActivity(1, "Zip Zap Zop");
        
        _repositoryMock
            .Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(activity);

        // Act
        var result = await _service.DeleteActivityAsync(1);

        // Assert
        result.Should().BeTrue();
        
        _repositoryMock.Verify(r => r.DeleteAsync(activity, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteActivityAsync_ReturnsFalse_WhenActivityNotFound()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync(999, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ImprovActivity?)null);

        // Act
        var result = await _service.DeleteActivityAsync(999);

        // Assert
        result.Should().BeFalse();
        
        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<ImprovActivity>(), It.IsAny<CancellationToken>()), Times.Never);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    private static ImprovActivity CreateTestActivity(int id, string name)
    {
        return new ImprovActivity
        {
            Id = id,
            Name = name,
            ActivityTypeId = 1,
            ActivityType = new ActivityType { Id = 1, Name = "Game", Description = "Test", IsActive = true, CreatedById = "test", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            Description = "Test description",
            Rules = "Test rules",
            Category = "Test",
            CreatedById = "user123",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
