using X39.Systems.ServiceOrchestrator.Data.Entity;

namespace X39.Systems.ServiceOrchestrator.Designer.Contract.Repositories;

public interface IServiceRepository
{
    IAsyncEnumerable<Service> GetServicesAsync();
    Task<Service> CreateServiceAsync(CancellationToken cancellationToken, Service service);
}