using Microsoft.AspNetCore.Builder;

namespace X39.Util.AspNet.Authorize;

public static class ApplicationBuilderExtensions {
    /// <summary>
    /// After UseAuthorization
    /// after UseAuthentication
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>Must be called after UseAuthorization</item>
    /// <item>Must be called after UseAuthentication</item>
    /// <item>Requires <see cref="ServiceCollectionExtensions.AddJwtLogin{TUserService}"/> to work.</item>
    /// </list>
    /// </remarks>
    /// <param name="applicationBuilder"></param>
    public static void UseJwtLoginMiddleware(
        this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<IJwtLoginMiddleware>();
    }
}