using ECommerce.Shared.DTOs.Product;

namespace ECommerce.API.Services.Interfaces;

public interface IProductService
{
    Task<IReadOnlyCollection<ProductSummaryDto>> GetActiveAsync(CancellationToken cancellationToken = default);
}
