using X39.Systems.ServiceOrchestrator.Data.UtilityInterfaces;
using X39.Util.Blazor.Attributes;
using X39.Util.Threading;

namespace X39.Systems.ServiceOrchestrator.Designer.Services.Storages;

[Singleton(ServiceType = typeof(IStorage))]
public sealed class InMemoryStorage : IStorage, IDisposable
{
    private readonly Dictionary<Type, List<object>> _data = new();
    private readonly ReaderWriterLockSlim _lock = new();

    private List<object> GetList<T>()
    {
        return _lock.UpgradeableReadLocked(() =>
        {
            if (_data.TryGetValue(typeof(T), out var list))
                return list;
            return _lock.WriteLocked(() =>
            {
                if (_data.TryGetValue(typeof(T), out list))
                    return list;
                return _data[typeof(T)] = new List<object>();
            });
        });
    }
#pragma warning disable CS1998
    public async IAsyncEnumerable<T> ReadAll<T>()
        where T : IHasGuid
#pragma warning restore CS1998
    {
        foreach (var t in GetList<T>().Cast<T>())
        {
            yield return t;
        }
    }

    public void Dispose()
    {
        _lock.Dispose();
    }
}