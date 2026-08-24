namespace ECommerce.API.DTOs.Cart;

public sealed class CartTotalsDto
{
    public decimal Subtotal { get; init; }
    public decimal Discount { get; init; }
    public decimal Tax { get; init; }
    public decimal Shipping { get; init; }
    public decimal Total { get; init; }
}
