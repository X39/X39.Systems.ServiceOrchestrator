using X39.Systems.ServiceOrchestrator.Data.Entity;

namespace X39.Systems.ServiceOrchestrator.Designer.Repositories;

public interface IStaticDataRepository
{
    IAsyncEnumerable<StaticData> GetStaticDatasAsync();
    Task<StaticData> CreateStaticDataAsync(CancellationToken cancellationToken, StaticData staticData);
}