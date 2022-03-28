using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace X39.Util.AspNet.Authorize;

[PublicAPI]
public class JwtAuthAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    public string[] Roles { get; }

    public JwtAuthAttribute(params string[] roles) : base(Constants.AuthenticationScheme)
    {
        Roles = roles;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Items[Constants.HttpContextUserItemName] as IUser;
        var userService = context.HttpContext.Items[Constants.HttpContextUserServiceItemName] as IJwtLoginService;
        if (user is null || userService is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!await userService.IsUserHavingRoleAsync(user, Roles))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}