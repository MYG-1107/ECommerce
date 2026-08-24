namespace ECommerce.Client.Services;

public sealed class ApiClient : IApiClient
{
    public HttpClient HttpClient { get; }

    public ApiClient(IConfiguration configuration)
    {
        var baseUrl = configuration["Api:BaseUrl"] ?? "https://localhost:7001";
        HttpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }
}
