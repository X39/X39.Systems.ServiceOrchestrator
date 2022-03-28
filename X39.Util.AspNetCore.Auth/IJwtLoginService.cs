using JetBrains.Annotations;

namespace X39.Util.AspNet.Authorize;

[PublicAPI]
public interface IJwtLoginService
{
    ValueTask<IUser?> GetUserByClaimsAsync(IDictionary<string, object> claims);
    ValueTask<bool> IsUserHavingRoleAsync(IUser user, IEnumerable<string> roles);
    
    /// <summary>
    /// Callback set by the <see cref="JwtLoginMiddleware{TJwtLoginService}"/>.
    /// Guaranteed to be non-null if setup is valid.
    /// </summary>
    GenerateJwtTokenDelegate? GenerateJwtToken { set; }
}