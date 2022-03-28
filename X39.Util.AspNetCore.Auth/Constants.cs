namespace X39.Util.AspNet.Authorize;

public static class Constants
{
    public const string HttpContextUserItemName = nameof(IJwtLoginMiddleware) + "-User";
    public const string HttpContextUserServiceItemName = nameof(IJwtLoginMiddleware) + "-UserService";
    public const string HttpContextClaimsIdentityItemName = nameof(IJwtLoginMiddleware) + "-ClaimsIdentity";
    public const string AuthenticationScheme = "JWT-Authentication";
}