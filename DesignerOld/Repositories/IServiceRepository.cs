using X39.Systems.ServiceOrchestrator.Data.Entity;

namespace X39.Systems.ServiceOrchestrator.Designer.Repositories;

public interface IServiceRepository
{
    IAsyncEnumerable<Orchestration> GetServicesAsync();
    Task<Orchestration> CreateServiceAsync(CancellationToken cancellationToken, Orchestration orchestration);
}