using X39.Systems.ServiceOrchestrator.Designer.Data;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Services.Impl;

[Singleton(ServiceType = typeof(INavigationService))]
public sealed class NavigationService : INavigationService
{
    public IEnumerable<INavigationContainer> NavigationContainers => _navigationContainers.AsReadOnly();
    private readonly List<INavigationContainer> _navigationContainers = new();
}