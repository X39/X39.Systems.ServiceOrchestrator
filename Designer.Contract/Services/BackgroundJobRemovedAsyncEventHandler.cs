using X39.Systems.ServiceOrchestrator.Designer.Contract.Data;

namespace X39.Systems.ServiceOrchestrator.Designer.Contract.Services;

public delegate Task BackgroundJobRemovedAsyncEventHandler(
    IBackgroundService backgroundService, BackgroundJob backgroundJob, Task task);