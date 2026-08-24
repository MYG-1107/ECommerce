namespace ECommerce.API.Services.Interfaces;

public interface IPaymentGateway
{
    Task<string> CreatePaymentIntentAsync(decimal amount, string currency, CancellationToken cancellationToken = default);
}
