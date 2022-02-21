using X39.Systems.ServiceOrchestrator.Designer.Contract.Data;

namespace X39.Systems.ServiceOrchestrator.Designer.Contract.Services;

public static class BackgroundService
{
    public static void Add(
        this IBackgroundService backgroundService,
        string text,
        Func<CancellationToken, BackgroundJob, Task> action)
    {
        backgroundService.Add(new BackgroundJob(action) {Text = text});
    }
    public static void Add(
        this IBackgroundService backgroundService,
        string text,
        Func<BackgroundJob, Task> action)
    {
        backgroundService.Add(new BackgroundJob(action) {Text = text});
    }
}