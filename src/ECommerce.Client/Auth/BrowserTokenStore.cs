using Microsoft.JSInterop;

namespace ECommerce.Client.Auth;

public sealed class BrowserTokenStore(IJSRuntime jsRuntime) : ITokenStore
{
    private const string AccessTokenKey = "ecommerce.access_token";

    public ValueTask SetAccessTokenAsync(string token)
        => jsRuntime.InvokeVoidAsync("localStorage.setItem", AccessTokenKey, token);

    public ValueTask<string?> GetAccessTokenAsync()
        => jsRuntime.InvokeAsync<string?>("localStorage.getItem", AccessTokenKey);

    public ValueTask ClearAsync()
        => jsRuntime.InvokeVoidAsync("localStorage.removeItem", AccessTokenKey);
}
