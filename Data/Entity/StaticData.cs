using X39.Systems.ServiceOrchestrator.Data.Structure;
using X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;

namespace X39.Systems.ServiceOrchestrator.Data.Entity;

public readonly record struct StaticData : IOutputEntity
{
    public string Name { get; init; } = string.Empty;
    public Guid Id { get; init; }
    public IReadOnlyCollection<EntityLink> OutputLinks { get; init; } = Array.Empty<EntityLink>();
    public byte[] Data { get; init; }
}