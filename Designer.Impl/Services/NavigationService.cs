using X39.Systems.ServiceOrchestrator.Designer.Contract.Data;
using X39.Systems.ServiceOrchestrator.Designer.Contract.Services;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Impl.Services;

[Singleton(ServiceType = typeof(INavigationService))]
public sealed class NavigationService : INavigationService
{
    public IEnumerable<INavigationContainer> NavigationContainers => _navigationContainers.AsReadOnly();
    private List<INavigationContainer> _navigationContainers = new();
}