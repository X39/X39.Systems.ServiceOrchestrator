using X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;

namespace X39.Systems.ServiceOrchestrator.Data.Entity;

public readonly record struct RestEndpoint : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public ERequestType RequestType { get; init; }
    public string Path { get; init; } = string.Empty;
    public IReadOnlyCollection<QueryParameter> QueryParameters { get; init; } = Array.Empty<QueryParameter>();
}