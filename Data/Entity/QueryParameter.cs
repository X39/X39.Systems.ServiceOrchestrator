namespace X39.Systems.ServiceOrchestrator.Data.Entity;

public readonly record struct QueryParameter
{
    public string Name { get; init; } = string.Empty;
    public  EQueryType QueryType { get; init; }
}