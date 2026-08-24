using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace ECommerce.Client.Auth;

public sealed class JwtAuthenticationStateProvider(ITokenStore tokenStore) : AuthenticationStateProvider
{
    private static readonly AuthenticationState Anonymous = new(new ClaimsPrincipal(new ClaimsIdentity()));

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await tokenStore.GetAccessTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
        {
            return Anonymous;
        }

        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "AuthenticatedUser") }, "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task MarkUserAsAuthenticatedAsync(string token)
    {
        await tokenStore.SetAccessTokenAsync(token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task MarkUserAsLoggedOutAsync()
    {
        await tokenStore.ClearAsync();
        NotifyAuthenticationStateChanged(Task.FromResult(Anonymous));
    }
}
