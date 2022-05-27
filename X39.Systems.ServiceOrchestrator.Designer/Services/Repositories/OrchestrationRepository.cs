using X39.Systems.ServiceOrchestrator.Data.Entity;
using X39.Systems.ServiceOrchestrator.Designer.Services.Storages;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Services.Repositories;

[Singleton]
public sealed class OrchestrationRepository : RepositoryBase<Orchestration>
{
    public OrchestrationRepository(IStorage storage) : base(storage)
    {
    }
}