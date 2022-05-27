using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;
using MudBlazor;
using X39.Systems.ServiceOrchestrator.Data.Entity;
using X39.Systems.ServiceOrchestrator.Designer.Data;

namespace X39.Systems.ServiceOrchestrator.Designer.Tabs.OrchestrationTab;

public class OrchestrationTab : ITabHost
{
    private readonly Orchestration _orchestration;
    public string TabIcon => Icons.Filled.Pages;
    public Type TabComponentType => typeof(OrchestrationTabComponent);
    public string TabName => _orchestration.Title;
    public Guid Guid => _orchestration.Guid;
    public IDictionary<string, object>? TabComponentParameters { get; }

    public OrchestrationTab(Orchestration orchestration)
    {
        _orchestration = orchestration;
        TabComponentParameters = new Dictionary<string, object>
        {
            {nameof(OrchestrationTabComponent.Service), _orchestration},
            {nameof(OrchestrationTabComponent.Host), this},
        };
    }

    public async Task OnAddedAsync(CancellationToken cancellationToken)
    {
    }

    public async Task OnRemovedAsync(CancellationToken cancellationToken)
    {
    }

    public void LinkAdded(BaseLinkModel obj)
    {
        throw new NotImplementedException();
    }

    public void LinkRemoved(BaseLinkModel obj)
    {
        throw new NotImplementedException();
    }

    public void NodeAdded(NodeModel obj)
    {
        throw new NotImplementedException();
    }

    public void NodeRemoved(NodeModel obj)
    {
        throw new NotImplementedException();
    }
}