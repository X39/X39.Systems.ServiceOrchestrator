using JetBrains.Annotations;
using X39.Systems.ServiceOrchestrator.Designer.Services;

// ReSharper disable RedundantDefaultMemberInitializer

namespace X39.Systems.ServiceOrchestrator.Designer.Data;

/// <summary>
/// A container for <see cref="IBackgroundService"/>'s.
/// </summary>
[PublicAPI]
public class BackgroundJob
{
    /// <summary>
    /// Whether this <see cref="BackgroundJob"/>'s <see cref="Callback"/> has a <see cref="CancellationToken"/>
    /// associated with it or not.
    /// </summary>
    public bool Cancellable { get; }

    /// <summary>
    /// The callback of this <see cref="BackgroundJob"/>.
    /// </summary>
    public Func<CancellationToken, BackgroundJob, Task> Callback { get; }

    /// <summary>
    /// Creates a new <see cref="BackgroundJob"/> with the given <paramref name="action"/> to process.
    /// </summary>
    /// <remarks>
    /// <see cref="BackgroundJob"/>'s automatically get removed once the <paramref name="action"/> completed.
    /// <see cref="BackgroundJob"/> created with this constructor will not be cancellable as no
    /// <see cref="CancellationToken"/> is passed into the action.
    /// </remarks>
    /// <param name="action">
    /// Delegate performing the background job,
    /// receiving this instance of <see cref="BackgroundJob"/>.
    /// </param>
    public BackgroundJob(Func<BackgroundJob, Task> action)
    {
        Callback = (_, job) => action(job);
        Cancellable = false;
    }

    /// <summary>
    /// Creates a new <see cref="BackgroundJob"/> with the given <paramref name="action"/> to process.
    /// </summary>
    /// <remarks>
    /// <see cref="BackgroundJob"/>'s automatically get removed once the <paramref name="action"/> completed.
    /// </remarks>
    /// <param name="action">
    /// Delegate performing the background job,
    /// receiving this instance of <see cref="BackgroundJob"/> and a <see cref="CancellationToken"/>,
    /// allowing a user to possibly cancel a <see cref="BackgroundJob"/> at any point in time.
    /// </param>
    public BackgroundJob(Func<CancellationToken, BackgroundJob, Task> action)
    {
        Callback = action;
        Cancellable = true;
    }

    /// <summary>
    /// The text to display regarding the current task.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// The minimum value for progress display.
    /// Must be less then or equal to <see cref="Value"/>,
    /// otherwise the behavior is undefined.
    /// </summary>
    /// <remarks>
    /// Defaults to 0.
    /// </remarks>
    public float MinValue { get; set; } = 0;

    /// <summary>
    /// The current value of progress.
    /// Must sit between or at <see cref="MinValue"/> and <see cref="MaxValue"/>,
    /// otherwise the behavior is undefined.
    /// </summary>
    public float Value { get; set; } = 0;

    /// <summary>
    /// The maximum value for progress display.
    /// Must be larger then or equal to <see cref="Value"/>,
    /// otherwise the behavior is undefined.
    /// </summary>
    /// <remarks>
    /// Defaults to 1.
    /// </remarks>
    public float MaxValue { get; set; } = 1;

    /// <summary>
    /// Whether the progress can be defined in numbers or not.
    /// </summary>
    public bool IsIndeterminate { get; set; } = true;
}