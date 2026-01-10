using FluentAssertions;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Validators;

namespace ImprovByExample.UnitTests.Application.Validators;

public class CreateActivityDtoValidatorTests
{
    private readonly CreateActivityDtoValidator _validator = new();

    [Fact]
    public void Validate_WithValidData_ShouldPass()
    {
        // Arrange
        var dto = new CreateActivityDto
        {
            Name = "Test Activity",
            ActivityTypeId = 1,
            Description = "Test description",
            Rules = "Test rules",
            Category = "Test"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithEmptyName_ShouldFail()
    {
        // Arrange
        var dto = new CreateActivityDto
        {
            Name = "",
            ActivityTypeId = 1,
            Description = "Test description",
            Rules = "Test rules",
            Category = "Test"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public void Validate_WithTooLongName_ShouldFail()
    {
        // Arrange
        var dto = new CreateActivityDto
        {
            Name = new string('a', 201),
            ActivityTypeId = 1,
            Description = "Test description",
            Rules = "Test rules",
            Category = "Test"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Name");
    }

    [Fact]
    public void Validate_WithInvalidActivityTypeId_ShouldFail()
    {
        // Arrange
        var dto = new CreateActivityDto
        {
            Name = "Test Activity",
            ActivityTypeId = 0,
            Description = "Test description",
            Rules = "Test rules",
            Category = "Test"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "ActivityTypeId");
    }

    [Fact]
    public void Validate_WithEmptyDescription_ShouldFail()
    {
        // Arrange
        var dto = new CreateActivityDto
        {
            Name = "Test Activity",
            ActivityTypeId = 1,
            Description = "",
            Rules = "Test rules",
            Category = "Test"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Description");
    }

    [Fact]
    public void Validate_WithMaxPlayersLessThanMinPlayers_ShouldFail()
    {
        // Arrange
        var dto = new CreateActivityDto
        {
            Name = "Test Activity",
            ActivityTypeId = 1,
            Description = "Test description",
            Rules = "Test rules",
            Category = "Test",
            MinPlayers = 5,
            MaxPlayers = 3
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "MaxPlayers");
    }

    [Fact]
    public void Validate_WithNegativeMinPlayers_ShouldFail()
    {
        // Arrange
        var dto = new CreateActivityDto
        {
            Name = "Test Activity",
            ActivityTypeId = 1,
            Description = "Test description",
            Rules = "Test rules",
            Category = "Test",
            MinPlayers = 0
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "MinPlayers");
    }
}
