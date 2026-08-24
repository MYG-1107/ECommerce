using ECommerce.API.Data;
using ECommerce.API.Models;
using ECommerce.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Repositories;

public sealed class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{
    public Task<List<Category>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.Categories
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}
