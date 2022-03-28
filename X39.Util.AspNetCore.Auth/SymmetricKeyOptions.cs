using JetBrains.Annotations;

namespace X39.Util.AspNet.Authorize;

[PublicAPI]
public class SymmetricKeyOptions
{
    public string Key { get; set; } = string.Empty;
}