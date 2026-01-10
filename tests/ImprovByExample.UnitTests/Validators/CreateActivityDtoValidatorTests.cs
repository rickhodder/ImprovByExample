using FluentAssertions;
using FluentValidation.TestHelper;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Validators;
using Xunit;

namespace ImprovByExample.UnitTests.Validators;

public class CreateActivityDtoValidatorTests
{
    private readonly CreateActivityDtoValidator _validator;

    public CreateActivityDtoValidatorTests()
    {
        _validator = new CreateActivityDtoValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        // Arrange
        var dto = new CreateActivityDto { Name = string.Empty };

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Activity name is required");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Exceeds_MaxLength()
    {
        // Arrange
        var dto = new CreateActivityDto { Name = new string('a', 201) };

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name)
            .WithErrorMessage("Activity name cannot exceed 200 characters");
    }

    [Fact]
    public void Should_Have_Error_When_ActivityTypeId_Is_Zero()
    {
        // Arrange
        var dto = new CreateActivityDto { ActivityTypeId = 0 };

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ActivityTypeId)
            .WithErrorMessage("Activity type is required");
    }

    [Fact]
    public void Should_Have_Error_When_MinPlayers_Greater_Than_MaxPlayers()
    {
        // Arrange
        var dto = new CreateActivityDto 
        { 
            Name = "Test Activity",
            ActivityTypeId = 1,
            Description = "Test",
            Rules = "Test rules",
            Category = "Test",
            MinPlayers = 10,
            MaxPlayers = 5
        };

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MaxPlayers)
            .WithErrorMessage("Maximum players must be greater than or equal to minimum players");
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Required_Fields_Are_Valid()
    {
        // Arrange
        var dto = new CreateActivityDto
        {
            Name = "Zip Zap Zop",
            ActivityTypeId = 1,
            Description = "A classic improv warmup game",
            Rules = "Stand in a circle and pass the energy",
            Category = "Warmup",
            MinPlayers = 3,
            MaxPlayers = 20,
            DurationMinutes = 5
        };

        // Act
        var result = _validator.TestValidate(dto);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
