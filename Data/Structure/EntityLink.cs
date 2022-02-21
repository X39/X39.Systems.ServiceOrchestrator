using X39.Systems.ServiceOrchestrator.Data.Entity;

namespace X39.Systems.ServiceOrchestrator.Data.Structure;

public readonly record struct EntityLink
{
    public Guid TargetId { get; init; }
    public Guid SourceId { get; init; }
}