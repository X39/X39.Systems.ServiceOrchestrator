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
    private readonly List<Service> _data = new List<Service>()
    {
        new() {Id = Guid.NewGuid(), Name = "Service 1"},
        new() {Id = Guid.NewGuid(), Name = "Service 2"},
        new() {Id = Guid.NewGuid(), Name = "Service 3"},
        new() {Id = Guid.NewGuid(), Name = "Service 4"},
    };

    public async IAsyncEnumerable<Service> GetServicesAsync()
    {
        foreach (var service in _data)
            yield return service;
    }

    public Task<Service> CreateServiceAsync(CancellationToken cancellationToken, Service service)
    {
        service = service with {Id = Guid.NewGuid()};
        _data.Add(service);
        return Task.FromResult(service);
    }
}
#pragma warning restore CS1998