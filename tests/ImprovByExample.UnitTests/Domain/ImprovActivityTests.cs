using ImprovByExample.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace ImprovByExample.UnitTests.Domain;

public class ImprovActivityTests
{
    [Fact]
    public void ImprovActivity_Should_Initialize_With_Default_Values()
    {
        // Act
        var activity = new ImprovActivity();

        // Assert
        activity.Id.Should().Be(0);
        activity.Name.Should().BeEmpty();
        activity.Tags.Should().BeEmpty();
        activity.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        activity.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void ImprovActivity_Should_Allow_Setting_Properties()
    {
        // Arrange
        var activity = new ImprovActivity();
        var tags = new[] { "warmup", "focus" };

        // Act
        activity.Name = "Zip Zap Zop";
        activity.Description = "Energy passing game";
        activity.Rules = "Stand in circle";
        activity.Category = "Warmup";
        activity.MinPlayers = 3;
        activity.MaxPlayers = 20;
        activity.DurationMinutes = 5;
        activity.Tags = tags;

        // Assert
        activity.Name.Should().Be("Zip Zap Zop");
        activity.Description.Should().Be("Energy passing game");
        activity.Rules.Should().Be("Stand in circle");
        activity.Category.Should().Be("Warmup");
        activity.MinPlayers.Should().Be(3);
        activity.MaxPlayers.Should().Be(20);
        activity.DurationMinutes.Should().Be(5);
        activity.Tags.Should().BeEquivalentTo(tags);
    }
}
