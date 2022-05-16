using X39.Systems.ServiceOrchestrator.Data.Entity;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Repositories.Impl;
#pragma warning disable CS1998
[Singleton(ServiceType = typeof(IAgentRepository), ConditionProperty = nameof(IsLoaded))]
public sealed class AgentRepository : IAgentRepository
{
#if DEBUG
    private static bool IsLoaded => true;
#else
    private static bool IsLoaded => false;
#endif

    // ReSharper disable once ArrangeObjectCreationWhenTypeEvident
    private readonly List<Agent> _data = new List<Agent>()
    {
        new() {Id = Guid.NewGuid(), Name = "Agent 1"},
        new() {Id = Guid.NewGuid(), Name = "Agent 2"},
        new() {Id = Guid.NewGuid(), Name = "Agent 3"},
        new() {Id = Guid.NewGuid(), Name = "Agent 4"},
    };

    public async IAsyncEnumerable<Agent> GetAgentsAsync()
    {
        foreach (var agent in _data)
            yield return agent;
    }

    public Task<Agent> CreateAgentAsync(CancellationToken cancellationToken, Agent agent)
    {
        agent = agent with {Id = Guid.NewGuid()};
        _data.Add(agent);
        return Task.FromResult(agent);
    }
}
#pragma warning restore CS1998