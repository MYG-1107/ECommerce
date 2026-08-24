using System.ComponentModel.DataAnnotations;
using ECommerce.API.DTOs.Auth;

namespace ECommerce.Tests;

public class AuthRequestValidationTests
{
    [Fact]
    public void RegisterRequest_WithMismatchedPasswords_FailsValidation()
    {
        var dto = new RegisterRequestDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test@example.com",
            Password = "Password1!",
            ConfirmPassword = "Password2!"
        };

        var results = Validate(dto);

        Assert.Contains(results, x => x.MemberNames.Contains(nameof(RegisterRequestDto.ConfirmPassword)));
    }

    [Fact]
    public void LoginRequest_WithoutPassword_FailsValidation()
    {
        var dto = new LoginRequestDto
        {
            Email = "test@example.com",
            Password = string.Empty
        };

        var results = Validate(dto);

        Assert.Contains(results, x => x.MemberNames.Contains(nameof(LoginRequestDto.Password)));
    }

    private static List<ValidationResult> Validate<T>(T model)
    {
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(model!, new ValidationContext(model!), validationResults, validateAllProperties: true);
        return validationResults;
    }
}
