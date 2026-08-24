using ECommerce.API.Repositories.Interfaces;
using ECommerce.API.Services.Interfaces;
using ECommerce.Shared.DTOs.Product;

namespace ECommerce.API.Services;

public sealed class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<IReadOnlyCollection<ProductSummaryDto>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        var products = await productRepository.GetActiveAsync(cancellationToken);
        return products.Select(x => new ProductSummaryDto
        {
            ProductId = x.ProductId,
            Name = x.Name,
            Slug = x.Slug,
            Price = x.Price,
            DiscountPrice = x.DiscountPrice,
            SKU = x.SKU,
            ImageUrl = x.ImageUrl
        }).ToArray();
    }
}
