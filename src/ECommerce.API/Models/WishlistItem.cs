namespace ECommerce.API.Models;

public class WishlistItem
{
    public int WishlistItemId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ApplicationUser? User { get; set; }
    public Product? Product { get; set; }
}
