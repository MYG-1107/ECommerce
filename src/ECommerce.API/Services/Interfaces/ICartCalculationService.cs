using ECommerce.API.DTOs.Cart;

namespace ECommerce.API.Services.Interfaces;

public interface ICartCalculationService
{
    CartTotalsDto Calculate(IEnumerable<CartLineDto> items, decimal taxRate, decimal shipping);
}
