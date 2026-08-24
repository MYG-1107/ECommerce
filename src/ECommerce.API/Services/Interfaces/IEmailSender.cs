namespace ECommerce.API.Services.Interfaces;

public interface IEmailSender
{
    Task SendForgotPasswordAsync(string email, string callbackUrl, CancellationToken cancellationToken = default);
}
