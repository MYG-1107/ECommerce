using ECommerce.API.DTOs.Cart;
using ECommerce.API.Services.Interfaces;

namespace ECommerce.API.Services;

public sealed class CartCalculationService : ICartCalculationService
{
    public CartTotalsDto Calculate(IEnumerable<CartLineDto> items, decimal taxRate, decimal shipping)
    {
        var lines = items.ToArray();
        var subtotal = lines.Sum(x => x.UnitPrice * x.Quantity);
        var discount = lines.Sum(x => x.DiscountPerUnit * x.Quantity);
        var taxable = Math.Max(0m, subtotal - discount);
        var tax = Math.Round(taxable * taxRate, 2, MidpointRounding.AwayFromZero);
        var total = taxable + tax + shipping;

        return new CartTotalsDto
        {
            Subtotal = subtotal,
            Discount = discount,
            Tax = tax,
            Shipping = shipping,
            Total = total
        };
    }
}
