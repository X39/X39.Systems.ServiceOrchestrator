using X39.Systems.ServiceOrchestrator.Data.Entity;
using X39.Systems.ServiceOrchestrator.Designer.Contract.Repositories;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Impl.Repositories;
#pragma warning disable CS1998
[Singleton(ServiceType = typeof(IRestEndpointRepository), ConditionProperty = nameof(IsLoaded))]
public sealed class RestEndpointRepository : IRestEndpointRepository
{
#if DEBUG
    private static bool IsLoaded => true;
#else
    private static bool IsLoaded => false;
#endif

    // ReSharper disable once ArrangeObjectCreationWhenTypeEvident
    private readonly List<RestEndpoint> _data = new List<RestEndpoint>()
    {
        new() {Id = Guid.NewGuid(), Name = "RestEndpoint 1"},
        new() {Id = Guid.NewGuid(), Name = "RestEndpoint 2"},
        new() {Id = Guid.NewGuid(), Name = "RestEndpoint 3"},
        new() {Id = Guid.NewGuid(), Name = "RestEndpoint 4"},
    };

    public async IAsyncEnumerable<RestEndpoint> GetRestEndpointsAsync()
    {
        foreach (var restEndpoint in _data)
            yield return restEndpoint;
    }

    public Task<RestEndpoint> CreateRestEndpointAsync(CancellationToken cancellationToken, RestEndpoint restEndpoint)
    {
        restEndpoint = restEndpoint with {Id = Guid.NewGuid()};
        _data.Add(restEndpoint);
        return Task.FromResult(restEndpoint);
    }
}
#pragma warning restore CS1998