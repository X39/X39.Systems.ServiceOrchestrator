namespace X39.Systems.ServiceOrchestrator.Data.Entity;

public record QueryParameter
{
    public string Name { get; init; } = string.Empty;
    public  EQueryType QueryType { get; init; }
}