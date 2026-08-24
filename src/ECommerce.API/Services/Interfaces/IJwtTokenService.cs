using ECommerce.API.Models;
using ECommerce.Shared.DTOs.Auth;

namespace ECommerce.API.Services.Interfaces;

public interface IJwtTokenService
{
    Task<AuthResultDto> CreateTokenAsync(ApplicationUser user, CancellationToken cancellationToken = default);
}
