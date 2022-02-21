using X39.Systems.ServiceOrchestrator.Data.Structure;

namespace X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;

public interface IOutputEntity : IEntity
{
    IReadOnlyCollection<EntityLink> OutputLinks { get; }
}