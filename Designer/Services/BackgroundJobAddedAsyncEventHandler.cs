using X39.Systems.ServiceOrchestrator.Designer.Data;

namespace X39.Systems.ServiceOrchestrator.Designer.Services;

public delegate Task BackgroundJobAddedAsyncEventHandler(
    IBackgroundService backgroundService, BackgroundJob backgroundJob, CancellationToken cancellationToken);