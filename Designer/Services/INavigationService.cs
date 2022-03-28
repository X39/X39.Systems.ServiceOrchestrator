using X39.Systems.ServiceOrchestrator.Designer.Data;

namespace X39.Systems.ServiceOrchestrator.Designer.Services;

public interface INavigationService
{
    IEnumerable<INavigationContainer> NavigationContainers { get; }
}