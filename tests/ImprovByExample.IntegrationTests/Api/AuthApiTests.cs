using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ImprovByExample.Application.Common.Models.DTOs;
using ImprovByExample.IntegrationTests.Common;

namespace ImprovByExample.IntegrationTests.Api;

public class AuthApiTests : IntegrationTestBase
{
    [Fact]
    public async Task Register_ReturnsOk_WithValidData()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Email = "newuser@test.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!",
            FirstName = "Test",
            LastName = "User"
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/register", registerDto);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.Should().NotBeNull();
        result.Should().ContainKey("userId");
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WithInvalidData()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Email = "invalid-email",
            Password = "weak",
            ConfirmPassword = "weak",
            FirstName = "Test",
            LastName = "User"
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/register", registerDto);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenPasswordsDontMatch()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Email = "test@test.com",
            Password = "Password123!",
            ConfirmPassword = "DifferentPassword123!",
            FirstName = "Test",
            LastName = "User"
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/register", registerDto);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Login_ReturnsOk_WithValidCredentials()
    {
        // Arrange - Use the seeded admin user
        var loginDto = new LoginDto
        {
            Email = "admin@improvbyexample.com",
            Password = "Admin123!",
            RememberMe = false
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/login", loginDto);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
        result.Should().NotBeNull();
        result.Should().ContainKey("email");
        result.Should().ContainKey("roles");
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WithInvalidCredentials()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Email = "admin@improvbyexample.com",
            Password = "WrongPassword123!",
            RememberMe = false
        };

        // Act
        var response = await Client.PostAsJsonAsync("/api/auth/login", loginDto);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetUser_ReturnsUnauthorized_WhenNotLoggedIn()
    {
        // Act
        var response = await Client.GetAsync("/api/auth/user");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Logout_ReturnsOk()
    {
        // Act
        var response = await Client.PostAsync("/api/auth/logout", null);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task FullAuthFlow_RegisterLoginLogout_WorksCorrectly()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            Email = "fullflow@test.com",
            Password = "Password123!",
            ConfirmPassword = "Password123!",
            FirstName = "Flow",
            LastName = "Test"
        };

        // Act & Assert - Register
        var registerResponse = await Client.PostAsJsonAsync("/api/auth/register", registerDto);
        registerResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Act & Assert - Login
        var loginDto = new LoginDto
        {
            Email = registerDto.Email,
            Password = registerDto.Password,
            RememberMe = false
        };
        var loginResponse = await Client.PostAsJsonAsync("/api/auth/login", loginDto);
        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Act & Assert - Get User (should work now)
        var getUserResponse = await Client.GetAsync("/api/auth/user");
        getUserResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Act & Assert - Logout
        var logoutResponse = await Client.PostAsync("/api/auth/logout", null);
        logoutResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        // Act & Assert - Get User (should fail after logout)
        var getUserAfterLogoutResponse = await Client.GetAsync("/api/auth/user");
        getUserAfterLogoutResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
