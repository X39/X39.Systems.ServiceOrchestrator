using X39.Systems.ServiceOrchestrator.Data.Structure;

namespace X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;

public interface IInputEntity : IEntity
{
    IReadOnlyCollection<EntityLink> InputLinks { get; }
}