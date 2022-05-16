namespace X39.Systems.ServiceOrchestrator.Designer.Data;

public interface ITabHost
{
    string TabIcon { get; }
    Type TabComponentType { get; }
    string TabName { get; }
    Guid Guid { get; }
    IDictionary<string, object>? TabComponentParameters { get; }
    Task OnAddedAsync(CancellationToken cancellationToken);
    Task OnRemovedAsync(CancellationToken cancellationToken);
}