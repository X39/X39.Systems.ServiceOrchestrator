using System.Globalization;

namespace X39.Systems.ServiceOrchestrator.Designer.Contract.Data;

public interface INavigationContainer
{
    /// <summary>
    /// The localized name of this navigation container.
    /// </summary>
    /// <remarks>
    /// Localization is expected to be done, using <see cref="CultureInfo.CurrentUICulture"/>.
    /// </remarks>
    string LocalizedName { get; }
    /// <summary>
    /// The target URL.
    /// </summary>
    string Target { get; }
}