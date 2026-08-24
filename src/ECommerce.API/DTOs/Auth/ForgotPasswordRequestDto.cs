using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.DTOs.Auth;

public sealed class ForgotPasswordRequestDto
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;
}
