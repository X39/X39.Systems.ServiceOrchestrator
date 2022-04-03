using X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;

namespace X39.Systems.ServiceOrchestrator.Data;

public record System : IHasName
{
    public string Name { get; init; } = string.Empty;
}