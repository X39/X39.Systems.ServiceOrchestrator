using X39.Systems.ServiceOrchestrator.Designer.Components.TabControl;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Services.Impl;

[Singleton(ServiceType = typeof(ITabService))]
public sealed class TabService : ITabService
{
    public event EventHandler<ITab>? TabAdded;
    public event EventHandler<ITab>? TabRemoved;

    private readonly List<ITab> _tabs = new();


    public IReadOnlyCollection<ITab> Tabs => _tabs.AsReadOnly();

    public void AddTab(ITab tab)
    {
        _tabs.Add(tab);
        TabAdded?.Invoke(this, tab);
    }

    public void RemoveTab(ITab tab)
    {
        if (_tabs.Remove(tab))
            TabRemoved?.Invoke(this, tab);
    }
}