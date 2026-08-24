namespace ECommerce.API.DTOs.Cart;

public sealed class CartLineDto
{
    public int ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal DiscountPerUnit { get; init; }
}
