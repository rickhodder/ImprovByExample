using FluentAssertions;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Validators;

namespace ImprovByExample.UnitTests.Application.Validators;

public class LoginDtoValidatorTests
{
    private readonly LoginDtoValidator _validator = new();

    [Fact]
    public void Validate_WithValidData_ShouldPass()
    {
        // Arrange
        var dto = new LoginDto
        {
            Email = "test@example.com",
            Password = "Test1234",
            RememberMe = false
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Validate_WithEmptyEmail_ShouldFail()
    {
        // Arrange
        var dto = new LoginDto
        {
            Email = "",
            Password = "Test1234"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email" && e.ErrorMessage.Contains("required"));
    }

    [Fact]
    public void Validate_WithInvalidEmail_ShouldFail()
    {
        // Arrange
        var dto = new LoginDto
        {
            Email = "notanemail",
            Password = "Test1234"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email" && e.ErrorMessage.Contains("valid email"));
    }

    [Fact]
    public void Validate_WithEmptyPassword_ShouldFail()
    {
        // Arrange
        var dto = new LoginDto
        {
            Email = "test@example.com",
            Password = ""
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Password" && e.ErrorMessage.Contains("required"));
    }

    [Fact]
    public void Validate_WithRememberMeTrue_ShouldPass()
    {
        // Arrange
        var dto = new LoginDto
        {
            Email = "test@example.com",
            Password = "Test1234",
            RememberMe = true
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
