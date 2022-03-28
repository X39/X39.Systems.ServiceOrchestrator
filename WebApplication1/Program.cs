using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X39.Util.AspNet.Authorize;

// https://stackoverflow.com/questions/36095076/custom-authentication-in-asp-net-core
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (options) =>
    {
    });
builder.Services.AddAuthentication((options) => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme);
//builder.Services.AddAuthorization((options) =>
//{
//    options.AddPolicy(Constants.AuthenticationScheme, (policyOptions) =>
//    {
//        policyOptions.RequireAuthenticatedUser();
//    });
//});
//builder.Services.AddAuthentication((options) =>
//{
//    options.DefaultChallengeScheme = Constants.AuthenticationScheme;
//    options.DefaultAuthenticateScheme = Constants.AuthenticationScheme;
//    options.AddScheme(Constants.AuthenticationScheme, (authenticationSChemeOptions) =>
//    {
//        authenticationSChemeOptions.HandlerType = typeof(MyAuthenticationHandler);
//    });
//});
//builder.Services.AddJwtLogin<IMyJwtLoginService, JwtLoginService>((options) =>
//{
//    options.Symmetric = new SymmetricKeyOptions
//    {
//        Key = "FANCY-SYMMETRIC-KEY-IS-NICE-FOR-TESTING",
//    };
//});
var app = builder.Build();
// app.UseJwtLoginMiddleware();
// app.UseAuthorization();
// app.UseAuthentication();
app.MapGet("/admin", [JwtAuthAttribute("admin")]() => "I AM ADMIN");
app.MapGet("/", () => "Hellou");
app.MapGet("/login", [AllowAnonymousAttribute]([FromQuery] string username, [FromQuery] string password, IMyJwtLoginService userService, HttpContext httpContext) =>
{
    if (userService.Login(httpContext.User, username, password) is { } token)
    {
        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.Name, "admin"));
        httpContext.User.AddIdentity(identity);
        return token;
    }

    return "Failure :(";
});

app.Run();

public interface IMyJwtLoginService : IJwtLoginService
{
    string? Login(ClaimsPrincipal claimsPrincipal, string username, string password);
}
public class JwtLoginService : IMyJwtLoginService
{
    private readonly List<User> _users = new()
    {
        new User {Name = "admin", Password = "admin", Roles = new[] {"admin"}},
        new User {Name = "user", Password = "user"},
    };

    public ValueTask<IUser?> GetUserByClaimsAsync(IDictionary<string, object> claims)
    {
        return claims.TryGetValue("nickname", out var value) && value is string nickname
            ? ValueTask.FromResult<IUser?>(_users.FirstOrDefault((q) => q.Name == nickname))
            : ValueTask.FromResult<IUser?>(null);
    }

    public ValueTask<bool> IsUserHavingRoleAsync(IUser user, IEnumerable<string> roles)
    {
        return user is User u
            ? ValueTask.FromResult(roles.All((role) => u.Roles.Contains(role)))
            : ValueTask.FromResult(false);
    }

    GenerateJwtTokenDelegate? IJwtLoginService.GenerateJwtToken
    {
        set => _generateJwtToken = value;
    }
    private GenerateJwtTokenDelegate? _generateJwtToken;

    public string? Login(ClaimsPrincipal claimsPrincipal, string username, string password)
    {
        if (_generateJwtToken is null)
            throw new InvalidOperationException("JWT-Login middleware setup is not complete.");
        var user = _users.FirstOrDefault((q) => q.Name == username && q.Password == password);
        return user is null ? null : _generateJwtToken(new Claim("Name", user.Name));
    }
}

internal class User : IUser
{
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
}

public class MyAuthenticationHandler : IAuthenticationHandler
{
    private readonly IMyJwtLoginService _loginService;
    private ClaimsIdentity? _claimsIdentity;
    private AuthenticationScheme? _scheme;

    public MyAuthenticationHandler(IMyJwtLoginService loginService)
    {
        _loginService = loginService;
    }
    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    {
        _scheme = scheme;
        _claimsIdentity = context.Items[Constants.HttpContextClaimsIdentityItemName] as ClaimsIdentity;
        return Task.CompletedTask;
    }

    public Task<AuthenticateResult> AuthenticateAsync()
    {
        var scheme = _scheme?.Name ?? throw new NullReferenceException($"{nameof(_scheme)} is null");
        return Task.FromResult(_claimsIdentity is null
            ? AuthenticateResult.NoResult()
            : AuthenticateResult.Success(
                new AuthenticationTicket(
                    new ClaimsPrincipal(_claimsIdentity),
                    new AuthenticationProperties(new Dictionary<string, string?>()
                    {
                        {"jwt", "asdasd"}
                    }),
                    scheme)));
    }

    public async Task ChallengeAsync(AuthenticationProperties? properties)
    {
    }

    public async Task ForbidAsync(AuthenticationProperties? properties)
    {
    }
}