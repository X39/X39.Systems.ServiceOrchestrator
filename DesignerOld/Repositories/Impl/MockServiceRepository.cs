using X39.Systems.ServiceOrchestrator.Data.Entity;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Repositories.Impl;
#pragma warning disable CS1998
[Singleton(ServiceType = typeof(IServiceRepository), ConditionProperty = nameof(IsLoaded))]
public sealed class ServiceRepository : IServiceRepository
{
#if DEBUG
    private static bool IsLoaded => true;
#else
    private static bool IsLoaded => false;
#endif

    // ReSharper disable once ArrangeObjectCreationWhenTypeEvident
    private readonly List<Orchestration> _data = new List<Orchestration>()
    {
        new() {Guid = Guid.NewGuid(), Title = "Service 1"},
        new() {Guid = Guid.NewGuid(), Title = "Service 2"},
        new() {Guid = Guid.NewGuid(), Title = "Service 3"},
        new() {Guid = Guid.NewGuid(), Title = "Service 4"},
    };

    public async IAsyncEnumerable<Orchestration> GetServicesAsync()
    {
        foreach (var service in _data)
            yield return service;
    }

    public Task<Orchestration> CreateServiceAsync(CancellationToken cancellationToken, Orchestration orchestration)
    {
        orchestration = orchestration with {Guid = Guid.NewGuid()};
        _data.Add(orchestration);
        return Task.FromResult(orchestration);
    }
}
#pragma warning restore CS1998