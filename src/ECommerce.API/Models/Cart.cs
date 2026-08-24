namespace ECommerce.API.Models;

public class Cart
{
    public int CartId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ApplicationUser? User { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = [];
}
