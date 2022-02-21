using X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;
namespace X39.Systems.ServiceOrchestrator.Data;

public readonly record struct System : IHasName
{
    public string Name { get; init; } = string.Empty;
}