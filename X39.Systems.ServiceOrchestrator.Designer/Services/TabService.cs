using System.Collections.ObjectModel;
using X39.Systems.ServiceOrchestrator.Designer.Data;
using X39.Util;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Services;

[Singleton]
public sealed class TabService
{
    public IReadOnlyCollection<ITabHost> Tabs => new ReadOnlyObservableCollection<ITabHost>(_tabs);
    private readonly ObservableCollection<ITabHost> _tabs = new();
    public ITabHost? SelectedTab { get; private set; }
    public event AsyncEventHandler<ITabHost>? SelectedTabChanged;
    public event AsyncEventHandler? TabsChanged;

    public async Task RemoveTabAsync(
        ITabHost tabHost,
        CancellationToken cancellationToken = default)
    {
        var removeResult = _tabs.Remove(tabHost);
        if (!removeResult)
            return;
        await tabHost.OnRemovedAsync(cancellationToken);
        await (TabsChanged?.DynamicInvokeAsync(this, EventArgs.Empty) ?? Task.CompletedTask);
    }

    public async Task AddTabAsync(
        ITabHost tabHost,
        CancellationToken cancellationToken = default)
    {
        if (_tabs.Contains(tabHost))
            return;
        _tabs.Add(tabHost);
        await tabHost.OnAddedAsync(cancellationToken);
        await (TabsChanged?.DynamicInvokeAsync(this, EventArgs.Empty) ?? Task.CompletedTask);
    }

    public async Task FocusAsync(ITabHost tabHost)
    {
        if (!_tabs.Contains(tabHost))
            throw new ArgumentException("Tab is not present in collection.");
        SelectedTab = tabHost;
        await (SelectedTabChanged?.DynamicInvokeAsync(this, tabHost) ?? Task.CompletedTask);
    }
}