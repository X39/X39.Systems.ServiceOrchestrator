using X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;

namespace X39.Systems.ServiceOrchestrator.Designer.Services.Storages;

public interface IStorage
{
    IAsyncEnumerable<T> ReadAll<T>()
        where T : IHasGuid;
}