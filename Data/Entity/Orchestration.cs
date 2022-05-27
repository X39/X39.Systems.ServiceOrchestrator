using X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;

namespace X39.Systems.ServiceOrchestrator.Data.Entity;

public record Orchestration : IEntity
{
    public Guid Guid { get; init; }
    public string Title { get; init; } = string.Empty;
}