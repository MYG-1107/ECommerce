using ECommerce.API.Auth;
using ECommerce.API.DTOs.Auth;
using ECommerce.API.Models;
using ECommerce.API.Services.Interfaces;
using ECommerce.Shared.Contracts;
using ECommerce.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.API.Services;

public sealed class AuthService(
    UserManager<ApplicationUser> userManager,
    IJwtTokenService jwtTokenService,
    IEmailSender emailSender) : IAuthService
{
    /// <summary>
    /// Registers a new customer user and returns an access token.
    /// </summary>
    public async Task<ApiResponse<AuthResultDto>> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default)
    {
        var userExists = await userManager.FindByEmailAsync(request.Email);
        if (userExists is not null)
        {
            return ApiResponse<AuthResultDto>.Fail("Email is already registered.");
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(" ", result.Errors.Select(x => x.Description));
            return ApiResponse<AuthResultDto>.Fail(errors);
        }

        await userManager.AddToRoleAsync(user, RoleConstants.Customer);
        var token = await jwtTokenService.CreateTokenAsync(user, cancellationToken);
        return ApiResponse<AuthResultDto>.Ok(token, "Registration successful");
    }

    /// <summary>
    /// Validates credentials and issues a JWT access token.
    /// </summary>
    public async Task<ApiResponse<AuthResultDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return ApiResponse<AuthResultDto>.Fail("Invalid credentials.");
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordValid)
        {
            return ApiResponse<AuthResultDto>.Fail("Invalid credentials.");
        }

        var token = await jwtTokenService.CreateTokenAsync(user, cancellationToken);
        return ApiResponse<AuthResultDto>.Ok(token, "Login successful");
    }

    public Task<ApiResponse<string>> RefreshAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ApiResponse<string>.Fail("Refresh token flow is scaffolded and pending implementation."));
    }

    public async Task<ApiResponse<string>> ForgotPasswordAsync(ForgotPasswordRequestDto request, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return ApiResponse<string>.Ok(string.Empty, "If the account exists, reset instructions have been sent.");
        }

        var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = $"https://example.local/reset-password?email={Uri.EscapeDataString(request.Email)}&token={Uri.EscapeDataString(resetToken)}";
        await emailSender.SendForgotPasswordAsync(request.Email, callbackUrl, cancellationToken);

        return ApiResponse<string>.Ok(string.Empty, "If the account exists, reset instructions have been sent.");
    }
}
