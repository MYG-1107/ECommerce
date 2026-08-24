namespace ECommerce.Shared.DTOs.Product;

public sealed class ProductSummaryDto
{
    public int ProductId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Slug { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal? DiscountPrice { get; init; }
    public string SKU { get; init; } = string.Empty;
    public string? ImageUrl { get; init; }
}
