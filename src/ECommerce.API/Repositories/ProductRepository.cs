using ECommerce.API.Data;
using ECommerce.API.Models;
using ECommerce.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repositories;

public sealed class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
{
    public Task<List<Product>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.Products
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}
