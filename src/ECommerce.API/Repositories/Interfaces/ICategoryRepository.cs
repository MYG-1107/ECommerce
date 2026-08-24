using ECommerce.API.Models;

namespace ECommerce.API.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetActiveAsync(CancellationToken cancellationToken = default);
}
