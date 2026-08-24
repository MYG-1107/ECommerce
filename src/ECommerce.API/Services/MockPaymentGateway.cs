using ECommerce.API.Services.Interfaces;

namespace ECommerce.API.Services;

public sealed class MockPaymentGateway : IPaymentGateway
{
    public Task<string> CreatePaymentIntentAsync(decimal amount, string currency, CancellationToken cancellationToken = default)
    {
        return Task.FromResult($"mock_payment_{currency}_{amount:0.00}_{Guid.NewGuid():N}");
    }
}
