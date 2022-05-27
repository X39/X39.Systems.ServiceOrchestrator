using JetBrains.Annotations;
using X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;
using X39.Systems.ServiceOrchestrator.Designer.Services.Storages;

namespace X39.Systems.ServiceOrchestrator.Designer.Services.Repositories;

[PublicAPI]
public abstract class RepositoryBase<TEntity>
    where TEntity : IEntity
{
    protected readonly IStorage Storage;

    public RepositoryBase(IStorage storage)
    {
        Storage = storage;
    }
    public IAsyncEnumerable<TEntity> GetAll()
    {
        return Storage.ReadAll<TEntity>();
    }
}