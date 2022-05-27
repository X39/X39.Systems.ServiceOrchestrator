using X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;

namespace X39.Systems.ServiceOrchestrator.Data;

public record System : IHasTitle
{
    public string Title { get; init; } = string.Empty;
}