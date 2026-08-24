using ECommerce.API.Models;

namespace ECommerce.API.Repositories.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetActiveAsync(CancellationToken cancellationToken = default);
}
