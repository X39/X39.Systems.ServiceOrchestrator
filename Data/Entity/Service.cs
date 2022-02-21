using X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;

namespace X39.Systems.ServiceOrchestrator.Data.Entity;

public readonly record struct Service : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}