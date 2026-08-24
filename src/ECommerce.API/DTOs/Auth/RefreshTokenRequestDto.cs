using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.DTOs.Auth;

public sealed class RefreshTokenRequestDto
{
    [Required]
    public string RefreshToken { get; init; } = string.Empty;
}
