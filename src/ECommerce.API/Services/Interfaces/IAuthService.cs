using ECommerce.API.DTOs.Auth;
using ECommerce.Shared.Contracts;
using ECommerce.Shared.DTOs.Auth;

namespace ECommerce.API.Services.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<AuthResultDto>> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<AuthResultDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<string>> RefreshAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken = default);
    Task<ApiResponse<string>> ForgotPasswordAsync(ForgotPasswordRequestDto request, CancellationToken cancellationToken = default);
}
