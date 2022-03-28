using JetBrains.Annotations;
using Microsoft.IdentityModel.Tokens;

namespace X39.Util.AspNet.Authorize;

[PublicAPI]
public class LoginJwtMiddlewareOptions
{
    /// <summary>
    /// Whether the key-containing properties (<see cref="Symmetric"/> and <see cref="Rsa"/>)
    /// should get their values using environment variables or not.
    /// </summary>
    /// <remarks>
    /// When environment variables are used, all <see cref="string"/> fields, normally providing the values,
    /// are assumed to be names of environment variables holding the actual value.
    /// </remarks>
    public bool UseEnvironmentVariables { get; set; }
    public SymmetricKeyOptions? Symmetric { get; set; }
    public RsaKeyOptions? Rsa { get; set; }

    /// <summary>
    /// The algorithm to use for signing the JWT's.
    /// See <see cref="SecurityAlgorithms"/> for the available strings.
    /// </summary>
    public string Algorithm { get; set; } = SecurityAlgorithms.HmacSha512Signature;
}