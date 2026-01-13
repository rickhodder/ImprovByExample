using FluentAssertions;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.Application.Validators;

namespace ImprovByExample.UnitTests.Application.Validators;

public class RegisterDtoValidatorTests
{
    private readonly RegisterDtoValidator _validator = new();

    [Fact]
    public void Validate_WithValidData_ShouldPass()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = "test@example.com",
            Password = "Test1234",
            ConfirmPassword = "Test1234",
            FirstName = "John",
            LastName = "Doe"
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
        var dto = new RegisterDto
        {
            Email = "",
            Password = "Test1234",
            ConfirmPassword = "Test1234"
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
        var dto = new RegisterDto
        {
            Email = "notanemail",
            Password = "Test1234",
            ConfirmPassword = "Test1234"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email" && e.ErrorMessage.Contains("valid email"));
    }

    [Fact]
    public void Validate_WithTooLongEmail_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = new string('a', 250) + "@test.com",
            Password = "Test1234",
            ConfirmPassword = "Test1234"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Email" && e.ErrorMessage.Contains("256 characters"));
    }

    [Fact]
    public void Validate_WithEmptyPassword_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = "test@example.com",
            Password = "",
            ConfirmPassword = ""
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Password" && e.ErrorMessage.Contains("required"));
    }

    [Fact]
    public void Validate_WithShortPassword_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = "test@example.com",
            Password = "Test1",
            ConfirmPassword = "Test1"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Password" && e.ErrorMessage.Contains("8 characters"));
    }

    [Fact]
    public void Validate_WithPasswordMissingDigit_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = "test@example.com",
            Password = "TestTest",
            ConfirmPassword = "TestTest"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Password" && e.ErrorMessage.Contains("digit"));
    }

    [Fact]
    public void Validate_WithPasswordMissingLowercase_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = "test@example.com",
            Password = "TEST1234",
            ConfirmPassword = "TEST1234"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Password" && e.ErrorMessage.Contains("lowercase"));
    }

    [Fact]
    public void Validate_WithPasswordMissingUppercase_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = "test@example.com",
            Password = "test1234",
            ConfirmPassword = "test1234"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Password" && e.ErrorMessage.Contains("uppercase"));
    }

    [Fact]
    public void Validate_WithPasswordsNotMatching_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = "test@example.com",
            Password = "Test1234",
            ConfirmPassword = "Different1234"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "ConfirmPassword" && e.ErrorMessage.Contains("must match"));
    }

    [Fact]
    public void Validate_WithTooLongFirstName_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = "test@example.com",
            Password = "Test1234",
            ConfirmPassword = "Test1234",
            FirstName = new string('a', 101)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "FirstName" && e.ErrorMessage.Contains("100 characters"));
    }

    [Fact]
    public void Validate_WithTooLongLastName_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = "test@example.com",
            Password = "Test1234",
            ConfirmPassword = "Test1234",
            LastName = new string('a', 101)
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "LastName" && e.ErrorMessage.Contains("100 characters"));
    }

    [Fact]
    public void Validate_WithOptionalFieldsNull_ShouldPass()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Email = "test@example.com",
            Password = "Test1234",
            ConfirmPassword = "Test1234",
            FirstName = null,
            LastName = null
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}
