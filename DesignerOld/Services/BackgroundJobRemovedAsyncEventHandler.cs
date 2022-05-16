using X39.Systems.ServiceOrchestrator.Designer.Data;

namespace X39.Systems.ServiceOrchestrator.Designer.Services;

public delegate Task BackgroundJobRemovedAsyncEventHandler(
    IBackgroundService backgroundService, BackgroundJob backgroundJob, Task task);