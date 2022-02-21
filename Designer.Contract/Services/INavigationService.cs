using X39.Systems.ServiceOrchestrator.Designer.Contract.Data;

namespace X39.Systems.ServiceOrchestrator.Designer.Contract.Services;

public interface INavigationService
{
    IEnumerable<INavigationContainer> NavigationContainers { get; }
}