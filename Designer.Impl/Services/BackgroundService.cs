using X39.Systems.ServiceOrchestrator.Designer.Contract.Data;
using X39.Systems.ServiceOrchestrator.Designer.Contract.Services;
using X39.Util;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Impl.Services;

[Singleton(ServiceType = typeof(IBackgroundService))]
public sealed class BackgroundService : IBackgroundService
{
    public event BackgroundJobAddedAsyncEventHandler? BackgroundJobAddedAsync;
    public event BackgroundJobRemovedAsyncEventHandler? BackgroundJobRemovedAsync;

    private readonly List<BackgroundJob> _backgroundJobs = new();

    public void Add(BackgroundJob backgroundJob)
    {
        lock (_backgroundJobs)
        {
            if (_backgroundJobs.Contains(backgroundJob))
                throw new ArgumentException($"{nameof(BackgroundJob)} is already present.");
            _backgroundJobs.Add(backgroundJob);
        }

        var cancellationTokenSource =
            backgroundJob.Cancellable ? new CancellationTokenSource() : null;

        var cancellationToken = cancellationTokenSource?.Token ?? CancellationToken.None;
        BackgroundJobAddedAsync
            ?.DynamicInvokeAsync(
                this,
                backgroundJob,
                cancellationToken)
            .ConfigureAwait(false);
        backgroundJob.Callback(cancellationToken, backgroundJob)
            .ContinueWith((task) =>
            {
                cancellationTokenSource?.Dispose();
                lock (_backgroundJobs)
                {
                    _backgroundJobs.Remove(backgroundJob);
                }
                BackgroundJobRemovedAsync?.DynamicInvokeAsync(
                    this, backgroundJob, task);
            }, CancellationToken.None);
    }
}