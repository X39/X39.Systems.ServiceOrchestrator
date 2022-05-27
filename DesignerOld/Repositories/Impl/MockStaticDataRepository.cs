using X39.Systems.ServiceOrchestrator.Data.Entity;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Repositories.Impl;
#pragma warning disable CS1998
[Singleton(ServiceType = typeof(IStaticDataRepository), ConditionProperty = nameof(IsLoaded))]
public sealed class StaticDataRepository : IStaticDataRepository
{
#if DEBUG
    private static bool IsLoaded => true;
#else
    private static bool IsLoaded => false;
#endif

    // ReSharper disable once ArrangeObjectCreationWhenTypeEvident
    private readonly List<StaticData> _data = new List<StaticData>()
    {
        new() {Guid = Guid.NewGuid(), Name = "StaticData 1"},
        new() {Guid = Guid.NewGuid(), Name = "StaticData 2"},
        new() {Guid = Guid.NewGuid(), Name = "StaticData 3"},
        new() {Guid = Guid.NewGuid(), Name = "StaticData 4"},
    };

    public async IAsyncEnumerable<StaticData> GetStaticDatasAsync()
    {
        foreach (var staticData in _data)
            yield return staticData;
    }

    public Task<StaticData> CreateStaticDataAsync(CancellationToken cancellationToken, StaticData staticData)
    {
        staticData = staticData with {Guid = Guid.NewGuid()};
        _data.Add(staticData);
        return Task.FromResult(staticData);
    }
}
#pragma warning restore CS1998