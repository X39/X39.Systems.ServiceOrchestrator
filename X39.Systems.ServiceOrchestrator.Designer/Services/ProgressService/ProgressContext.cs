using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

// ReSharper disable RedundantDefaultMemberInitializer

namespace X39.Systems.ServiceOrchestrator.Designer.Services.ProgressService;

public sealed class ProgressContext : IAsyncDisposable, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly Func<ProgressContext, ValueTask> _disposeCallback;
    private bool _isIndeterminate = true;
    private bool _canBuffer = false;
    private double _maxValue = 1.0;
    private double _minValue = 0.0;
    private double _value = 0.0;
    private double _bufferValue = 0.0;

    public ProgressContext(Func<ProgressContext, ValueTask> disposeCallback)
    {
        _disposeCallback = disposeCallback;
    }

    public bool IsIndeterminate
    {
        get => _isIndeterminate;
        set
        {
            _isIndeterminate = value;
            OnPropertyChanged();
        }
    }

    public bool CanBuffer
    {
        get => _canBuffer;
        set
        {
            _canBuffer = value;
            OnPropertyChanged();
        }
    }

    public double MaxValue
    {
        get => _maxValue;
        set
        {
            _maxValue = value;
            OnPropertyChanged();
        }
    }

    public double MinValue
    {
        get => _minValue;
        set
        {
            _minValue = value;
            OnPropertyChanged();
        }
    }

    public double Value
    {
        get => _value;
        set
        {
            _value = value;
            OnPropertyChanged();
        }
    }

    public double BufferValue
    {
        get => _bufferValue;
        set
        {
            _bufferValue = value;
            OnPropertyChanged();
        }
    }


    public ValueTask DisposeAsync()
    {
        return _disposeCallback(this);
    }


    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}