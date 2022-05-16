using System.Collections.ObjectModel;
using X39.Systems.ServiceOrchestrator.Designer.Data;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Services;

[Singleton]
public sealed class TabService
{
    public IReadOnlyCollection<ITabHost> Tabs => new ReadOnlyObservableCollection<ITabHost>(_tabs);
    private readonly ObservableCollection<ITabHost> _tabs = new();

    public async Task RemoveTabAsync(
        ITabHost tabHost,
        CancellationToken cancellationToken = default)
    {
        var removeResult = _tabs.Remove(tabHost);
        if (removeResult)
            await tabHost.OnRemovedAsync(cancellationToken);
    }

    public async Task AddTabAsync(
        ITabHost tabHost,
        CancellationToken cancellationToken = default)
    {
        if (_tabs.Contains(tabHost))
            return;
        _tabs.Add(tabHost);
        await tabHost.OnAddedAsync(cancellationToken);
    }
}