namespace ECommerce.API.Auth;

public static class RoleConstants
{
    public const string Admin = "Admin";
    public const string Customer = "Customer";

    public static readonly string[] AllRoles = [Admin, Customer];
}
