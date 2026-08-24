using ECommerce.API.DTOs.Cart;
using ECommerce.API.Services;

namespace ECommerce.Tests;

public class CartCalculationServiceTests
{
    [Fact]
    public void Calculate_ReturnsExpectedTotals()
    {
        var service = new CartCalculationService();
        var lines = new[]
        {
            new CartLineDto { ProductId = 1, Quantity = 2, UnitPrice = 50m, DiscountPerUnit = 5m },
            new CartLineDto { ProductId = 2, Quantity = 1, UnitPrice = 25m, DiscountPerUnit = 0m }
        };

        var result = service.Calculate(lines, taxRate: 0.10m, shipping: 7.5m);

        Assert.Equal(125m, result.Subtotal);
        Assert.Equal(10m, result.Discount);
        Assert.Equal(11.5m, result.Tax);
        Assert.Equal(7.5m, result.Shipping);
        Assert.Equal(134m, result.Total);
    }

    [Fact]
    public void Calculate_DoesNotAllowNegativeTaxableAmount()
    {
        var service = new CartCalculationService();
        var lines = new[]
        {
            new CartLineDto { ProductId = 1, Quantity = 1, UnitPrice = 10m, DiscountPerUnit = 20m }
        };

        var result = service.Calculate(lines, taxRate: 0.12m, shipping: 0m);

        Assert.Equal(10m, result.Subtotal);
        Assert.Equal(20m, result.Discount);
        Assert.Equal(0m, result.Tax);
        Assert.Equal(0m, result.Total);
    }
}
