using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.IntegrationTests.Common;

namespace ImprovByExample.IntegrationTests.Api;

public class ActivitiesApiTests : IntegrationTestBase
{
    [Fact]
    public async Task GetActivities_ReturnsOk_WithActivities()
    {
        // Act
        var response = await Client.GetAsync("/api/activities");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var activities = await response.Content.ReadFromJsonAsync<List<ActivityDto>>();
        activities.Should().NotBeNull();
        activities.Should().NotBeEmpty();
        activities.Should().HaveCountGreaterThan(0);
    }

    [Fact]
    public async Task GetActivityById_ReturnsOk_WhenActivityExists()
    {
        // Arrange - Get first activity from list
        var getAllResponse = await Client.GetAsync("/api/activities");
        var activities = await getAllResponse.Content.ReadFromJsonAsync<List<ActivityDto>>();
        var firstActivity = activities!.First();

        // Act
        var response = await Client.GetAsync($"/api/activities/{firstActivity.Id}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var activity = await response.Content.ReadFromJsonAsync<ActivityDto>();
        activity.Should().NotBeNull();
        activity!.Id.Should().Be(firstActivity.Id);
        activity.Name.Should().Be(firstActivity.Name);
    }

    [Fact]
    public async Task GetActivityById_ReturnsNotFound_WhenActivityDoesNotExist()
    {
        // Act
        var response = await Client.GetAsync("/api/activities/99999");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetActivities_CanFilterByType()
    {
        // Arrange - Get all activities to find a type that has activities
        var allActivitiesResponse = await Client.GetAsync("/api/activities");
        var allActivities = await allActivitiesResponse.Content.ReadFromJsonAsync<List<ActivityDto>>();
        var firstActivityType = allActivities!.First().ActivityTypeId;
        
        // Act - Filter by that type
        var response = await Client.GetAsync($"/api/activities?activityTypeId={firstActivityType}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var activities = await response.Content.ReadFromJsonAsync<List<ActivityDto>>();
        activities.Should().NotBeNull();
        activities.Should().NotBeEmpty();
        // All activities should have the specified type
        activities.Should().OnlyContain(a => a.ActivityTypeId == firstActivityType);
    }

    [Fact]
    public async Task SearchActivities_ReturnsMatchingActivities()
    {
        // Act - Search for "Zip"
        var response = await Client.GetAsync("/api/activities?search=Zip");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var activities = await response.Content.ReadFromJsonAsync<List<ActivityDto>>();
        activities.Should().NotBeNull();
        activities.Should().Contain(a => a.Name.Contains("Zip", StringComparison.OrdinalIgnoreCase));
    }
}
