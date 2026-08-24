namespace ECommerce.Shared.Contracts;

public class ValidationErrorResponse
{
    public string Title { get; init; } = "Validation failed";
    public int Status { get; init; } = 400;
    public Dictionary<string, string[]> Errors { get; init; } = new();
}
