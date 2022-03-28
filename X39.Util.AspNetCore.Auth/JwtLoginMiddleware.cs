using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace X39.Util.AspNet.Authorize;

[PublicAPI]
public class JwtLoginMiddleware<TJwtLoginService> : IJwtLoginMiddleware
    where TJwtLoginService : IJwtLoginService
{
    private readonly LoginJwtMiddlewareOptions _options;
    private readonly SecurityKey _securityKey;
    private readonly TJwtLoginService _jwtLoginService;

    public JwtLoginMiddleware(IOptions<LoginJwtMiddlewareOptions>? options, TJwtLoginService jwtLoginService)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _securityKey = CreateSecurityKey(_options);
        _jwtLoginService = jwtLoginService;
        _jwtLoginService.GenerateJwtToken = GenerateJwtToken;
    }

    /// <summary>
    /// Request handling method.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
    /// <param name="next">The delegate representing the remaining middleware in the request pipeline.</param>
    /// <returns>A <see cref="Task"/> that represents the execution of this middleware.</returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = context.Request.Headers["Authorization"]
            .FirstOrDefault()
            ?.Split(" ")
            .Last();
        if (!token.IsNullOrWhiteSpace())
        {
            await ValidateJwtTokenAndAttachUser(context, token);
        }

        await next(context);
    }

    private async Task ValidateJwtTokenAndAttachUser(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.FromMinutes(5),
            });
            context.Items[Constants.HttpContextUserServiceItemName] = _jwtLoginService;
            context.Items[Constants.HttpContextClaimsIdentityItemName] = validationResult.ClaimsIdentity;
            context.Items[Constants.HttpContextUserItemName] = await _jwtLoginService.GetUserByClaimsAsync(validationResult.Claims);
        }
        catch
        {
            // empty
        }
    }

    [PublicAPI]
    private string GenerateJwtToken(params Claim[] claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claimsIdentity = new ClaimsIdentity();
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                _securityKey,
                _options.Algorithm),
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    [Pure]
    private static SecurityKey CreateSecurityKey(LoginJwtMiddlewareOptions options)
    {
        string GetValue(string valueOrEnvName)
            => options.UseEnvironmentVariables
                ? Environment.GetEnvironmentVariable(valueOrEnvName) ?? string.Empty
                : valueOrEnvName;
        if (options.Symmetric is not null && !options.Symmetric.Key.IsNullOrWhiteSpace())
        {
            var key = GetValue(options.Symmetric.Key);
            var keyBytes = Encoding.UTF8.GetBytes(key);
            return new SymmetricSecurityKey(keyBytes);
        }

        // ReSharper disable once InvertIf
        if (options.Rsa is not null && !options.Rsa.PrivateKey.IsNullOrWhiteSpace())
        {
            var rsa = RSA.Create();
            switch (options.Rsa.IsPKCS8Key)
            {
                case true when options.Rsa.IsEncrypted:
                {
                    var privateKey = GetValue(options.Rsa.PrivateKey);
                    var privateKeyBytes = Encoding.UTF8.GetBytes(privateKey);
                    var password = GetValue(options.Rsa.Password);
                    var passwordBytes = Encoding.UTF8.GetBytes(password);
                    rsa.ImportEncryptedPkcs8PrivateKey(passwordBytes, privateKeyBytes, out _);
                    break;
                }
                case true:
                {
                    var privateKey = GetValue(options.Rsa.PrivateKey);
                    var privateKeyBytes = Encoding.UTF8.GetBytes(privateKey);
                    rsa.ImportPkcs8PrivateKey(privateKeyBytes, out _);
                    break;
                }
                default:
                {
                    var privateKey = GetValue(options.Rsa.PrivateKey);
                    var privateKeyBytes = Encoding.UTF8.GetBytes(privateKey);
                    rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
                    break;
                }
            }
            return new RsaSecurityKey(rsa.ExportParameters(true));
        }

        throw new InvalidOperationException(
            $"No SecurityKey configuration is present. " +
            $"Please check your configuration of {typeof(LoginJwtMiddlewareOptions).FullName}.");
    }
}