namespace ECommerce.Client.Auth;

public interface ITokenStore
{
    ValueTask SetAccessTokenAsync(string token);
    ValueTask<string?> GetAccessTokenAsync();
    ValueTask ClearAsync();
}
