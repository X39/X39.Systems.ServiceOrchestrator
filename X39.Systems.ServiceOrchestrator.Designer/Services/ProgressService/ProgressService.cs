using X39.Util;
using X39.Util.Blazor.Attributes;

namespace X39.Systems.ServiceOrchestrator.Designer.Services.ProgressService;

using System.Collections.ObjectModel;
using System.Collections.Specialized;

[Singleton]
public sealed class ProgressService
{
    private readonly List<ProgressContext> _progressContexts = new();
    public IReadOnlyCollection<ProgressContext> Contexts => _progressContexts.AsReadOnly();

    public event AsyncEventHandler<ProgressContext>? Started;
    public event AsyncEventHandler<ProgressContext>? Ended;
    public event AsyncEventHandler<ProgressContext>? ContextChanged;


    public async Task StartAsync(Func<ProgressContext, CancellationToken, Task> func)
    {
        await using var context = await NewContext();
        using var cancellationTokenSource = new CancellationTokenSource();
        await func(context, cancellationTokenSource.Token)
            .ConfigureAwait(false);
    }

    public void Run(Func<ProgressContext, CancellationToken, Task> func)
    {
        Task.Run(async () =>
        {
            await using var context = await NewContext();
            using var cancellationTokenSource = new CancellationTokenSource();
            await func(context, cancellationTokenSource.Token)
                .ConfigureAwait(false);
        });
    }

    public async Task<ProgressContext> NewContext(Action<ProgressContext>? configure = null)
    {
        var context = new ProgressContext(async (self) =>
        {
            _progressContexts.Remove(self);
            await (Ended?.DynamicInvokeAsync(this, self) ?? Task.CompletedTask);
        });
        context.PropertyChanged += (sender, args) =>
        {
            if (sender is not ProgressContext self)
                throw new Exception("Sender was expected to be of type ProgressContext.");
            _ = ContextChanged?.DynamicInvokeAsync(this, self);
        };
        configure?.Invoke(context);
        _progressContexts.Add(context);
        await (Started?.DynamicInvokeAsync(this, context) ?? Task.CompletedTask);
        return context;
    }
}