using X39.Systems.ServiceOrchestrator.Designer.Components.TabControl;

namespace X39.Systems.ServiceOrchestrator.Designer.Services;

public interface ITabService
{
    event EventHandler<ITab> TabAdded;
    event EventHandler<ITab> TabRemoved;
    
    IReadOnlyCollection<ITab> Tabs { get; }
    void AddTab(ITab tab);
    void RemoveTab(ITab tab);
}