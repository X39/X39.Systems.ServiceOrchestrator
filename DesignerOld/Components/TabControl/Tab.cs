namespace X39.Systems.ServiceOrchestrator.Designer.Components.TabControl;

public record Tab(Type HeaderComponentType, Type ContentComponentType) : ITab
{
    public IDictionary<string, object> HeaderParameters { get; } = new Dictionary<string, object>();
    public IDictionary<string, object> ContentParameters { get; } = new Dictionary<string, object>();
}