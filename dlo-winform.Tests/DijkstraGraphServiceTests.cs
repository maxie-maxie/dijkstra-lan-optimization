using dlo_winform;
using Graph;
using System.Drawing;

namespace dlo_winform.Tests;

public class DijkstraGraphServiceTests
{
    [Fact]
    public void FindRoute_ReturnsShortestPathUsingEdgeGraph()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(0, 0) };
        var b = new NetworkNode { Id = 2, Position = new PointF(10, 0) };
        var c = new NetworkNode { Id = 3, Position = new PointF(20, 0) };

        var data = new GraphData();
        data.nodeList.Add(a);
        data.nodeList.Add(b);
        data.nodeList.Add(c);
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = b, Weight = 5 });
        data.edgeList.Add(new NetworkEdge { StartNode = b, EndNode = c, Weight = 5 });
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = c, Weight = 20 });

        var result = DijkstraGraphService.FindRoute(data, 1, 3);

        Assert.True(result.Reachable);
        Assert.Equal(10L, result.TotalTime);
        Assert.Equal(new List<int> { 1, 2, 3 }, result.PathNodeIds);
    }

    [Fact]
    public void FindRoute_ReturnsUnreachableForIsolatedNode()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(0, 0) };
        var b = new NetworkNode { Id = 2, Position = new PointF(10, 0) };

        var data = new GraphData();
        data.nodeList.Add(a);
        data.nodeList.Add(b);
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = b, Weight = 5 });

        var result = DijkstraGraphService.FindRoute(data, 1, 999);

        Assert.False(result.Reachable);
    }
}

    [Fact]
    public void FindRoute_ReturnsUnreachableForIsolatedNode()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(0, 0) };
        var b = new NetworkNode { Id = 2, Position = new PointF(10, 0) };

        var data = new GraphData();
        data.nodeList.Add(a);
        data.nodeList.Add(b);
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = b, Weight = 5 });

        var result = DijkstraGraphService.FindRoute(data, 1, 999);

        Assert.False(result.Reachable);
    }
}
