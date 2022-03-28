using JetBrains.Annotations;

namespace X39.Util.AspNet.Authorize;

[PublicAPI]
public class RsaKeyOptions
{
    /// <summary>
    /// The private key contents (as in without BEGIN PRIVATE KEY or newlines).
    /// </summary>
    public string PrivateKey { get; set; } = string.Empty;

    /// <summary>
    /// If true, <see cref="Password"/> will be used to decrypt the <see cref="PrivateKey"/>.
    /// </summary>
    public bool IsEncrypted { get; set; }

    /// <summary>
    /// If true, the format is expected to be of a PKCS#8 key.
    /// Otherwise, PKCS#1 is assumed.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public bool IsPKCS8Key { get; set; }
    
    /// <summary>
    /// The password of <see cref="PrivateKey"/>.
    /// Will only be checked if <see cref="IsEncrypted"/> is true.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}