namespace X39.Systems.ServiceOrchestrator.Designer.Components.TabControl;

public interface ITab
{
    IDictionary<string,object>? HeaderParameters { get; }
    Type HeaderComponentType { get; }
    
    IDictionary<string,object>? ContentParameters { get; }
    Type ContentComponentType { get; }
}