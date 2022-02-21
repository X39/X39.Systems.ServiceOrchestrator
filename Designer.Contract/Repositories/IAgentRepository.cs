using X39.Systems.ServiceOrchestrator.Data.Entity;

namespace X39.Systems.ServiceOrchestrator.Designer.Contract.Repositories;

public interface IAgentRepository
{
    IAsyncEnumerable<Agent> GetAgentsAsync();
    Task<Agent> CreateAgentAsync(CancellationToken cancellationToken, Agent agent);
}