using X39.Systems.ServiceOrchestrator.Data.Entity;

namespace X39.Systems.ServiceOrchestrator.Designer.Contract.Repositories;

public interface IRestEndpointRepository
{
    IAsyncEnumerable<RestEndpoint> GetRestEndpointsAsync();
    Task<RestEndpoint> CreateRestEndpointAsync(CancellationToken cancellationToken, RestEndpoint restEndpoint);
}