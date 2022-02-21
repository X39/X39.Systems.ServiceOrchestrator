using X39.Systems.ServiceOrchestrator.Designer.Contract.Data;

namespace X39.Systems.ServiceOrchestrator.Designer.Contract.Services;

public delegate Task BackgroundJobAddedAsyncEventHandler(
    IBackgroundService backgroundService, BackgroundJob backgroundJob, CancellationToken cancellationToken);