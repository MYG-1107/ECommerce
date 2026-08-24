using ECommerce.API.Services.Interfaces;

namespace ECommerce.API.Services;

public sealed class DevelopmentEmailSender(ILogger<DevelopmentEmailSender> logger) : IEmailSender
{
    public Task SendForgotPasswordAsync(string email, string callbackUrl, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Forgot password placeholder email for {Email}. Reset URL: {ResetUrl}", email, callbackUrl);
        return Task.CompletedTask;
    }
}
