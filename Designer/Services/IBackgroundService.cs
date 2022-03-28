using JetBrains.Annotations;
using X39.Systems.ServiceOrchestrator.Designer.Data;

namespace X39.Systems.ServiceOrchestrator.Designer.Services;

[PublicAPI]
public interface IBackgroundService
{
    event BackgroundJobAddedAsyncEventHandler? BackgroundJobAddedAsync;
    event BackgroundJobRemovedAsyncEventHandler? BackgroundJobRemovedAsync;
    void Add(BackgroundJob backgroundJob);
}