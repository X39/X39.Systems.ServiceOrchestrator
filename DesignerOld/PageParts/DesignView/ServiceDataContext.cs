using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Blazor.Diagrams.Core;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using JetBrains.Annotations;
using X39.Systems.ServiceOrchestrator.Data.Entity;

namespace X39.Systems.ServiceOrchestrator.Designer.PageParts.DesignView;

public class ServiceDataContext : INotifyPropertyChanged
{
    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion

    private const int GridSize = 10;

    #region Properties

    public Orchestration Orchestration
    {
        get => _orchestration;
        set
        {
            _orchestration = value;
            RaisePropertyChanged();
        }
    }
    private Orchestration _orchestration;

    #endregion

    public Diagram Diagram { get; } = new(new DiagramOptions
    {
        DeleteKey = "Delete",
        AllowMultiSelection = true,
        AllowPanning = true,
        GridSize = GridSize,
        Links = new DiagramLinkOptions
        {
            DefaultPathGenerator = PathGenerators.Straight,
            DefaultRouter = Routers.Orthogonal,
        },
        Zoom = new DiagramZoomOptions
        {
            Minimum = 0.125,
            Maximum = 1,
            Inverse = true,
        },
    });

    public ServiceDataContext(Orchestration orchestration)
    {
        _orchestration = orchestration;
        Setup();
    }

    private static int i = 0;
    private void Setup()
    {
        var node1 = NewNode(50, 50);
        var node2 = NewNode(300, 300);
        var node3 = NewNode(300, 50);
        if (++i >= 1) Diagram.Nodes.Add(node1);
        if (++i >= 2) Diagram.Nodes.Add(node2);
        if (++i >= 3) Diagram.Nodes.Add(node3);
        Diagram.Links.Add(new LinkModel(node1.GetPort(PortAlignment.Right), node2.GetPort(PortAlignment.Left)));
    }

    private NodeModel NewNode(double x, double y)
    {
        var node = new NodeModel(new Point(x, y));
        node.AddPort(PortAlignment.Bottom);
        node.AddPort(PortAlignment.Top);
        node.AddPort(PortAlignment.Left);
        node.AddPort(PortAlignment.Right);
        return node;
    }
}