using Xunit;
using System.Drawing;
using dlo_winform;

namespace dlo_winform.Tests;

public class GraphEditorTests
{
    [Fact]
    public void TryGetEdgeNearPoint_ReturnsEdgeNearLineSegment()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(0, 0) };
        var b = new NetworkNode { Id = 2, Position = new PointF(100, 0) };
        var edge = new NetworkEdge { StartNode = a, EndNode = b, Weight = 5 };
        var data = new GraphData();
        data.edgeList.Add(edge);

        var result = GraphEditor.TryGetEdgeNearPoint(data, new PointF(50, 4), 6);

        Assert.Equal(edge, result);
    }

    [Fact]
    public void TryGetEdgeNearPoint_ReturnsNullForFarPoint()
    {
        var a = new NetworkNode { Id = 1, Position = new PointF(0, 0) };
        var b = new NetworkNode { Id = 2, Position = new PointF(100, 0) };
        var data = new GraphData();
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = b, Weight = 5 });

        var result = GraphEditor.TryGetEdgeNearPoint(data, new PointF(50, 50), 6);

        Assert.Null(result);
    }

    [Fact]
    public void ToggleEdge_AddsWhenMissing()
    {
        var a = new NetworkNode { Id = 1, Position = PointF.Empty };
        var b = new NetworkNode { Id = 2, Position = PointF.Empty };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b });

        GraphEditor.ToggleEdge(data, a, b);

        Assert.Single(data.edgeList);
    }

    [Fact]
    public void ToggleEdge_RemovesWhenExists()
    {
        var a = new NetworkNode { Id = 1, Position = PointF.Empty };
        var b = new NetworkNode { Id = 2, Position = PointF.Empty };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b });
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = b, Weight = 5 });

        GraphEditor.ToggleEdge(data, a, b);

        Assert.Empty(data.edgeList);
    }

    [Fact]
    public void ToggleEdge_ReversedNodesAlsoRemoves()
    {
        var a = new NetworkNode { Id = 1, Position = PointF.Empty };
        var b = new NetworkNode { Id = 2, Position = PointF.Empty };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b });
        data.edgeList.Add(new NetworkEdge { StartNode = a, EndNode = b, Weight = 5 });

        GraphEditor.ToggleEdge(data, b, a);

        Assert.Empty(data.edgeList);
    }

    [Fact]
    public void ToggleEdge_SameNodeDoesNothing()
    {
        var a = new NetworkNode { Id = 1, Position = PointF.Empty };
        var data = new GraphData();
        data.nodeList.Add(a);

        GraphEditor.ToggleEdge(data, a, a);

        Assert.Empty(data.edgeList);
    }

    [Fact]
    public void AddEdgeIfMissing_DoesNotAddDuplicateUndirectedEdge()
    {
        var a = new NetworkNode { Id = 1, Position = PointF.Empty };
        var b = new NetworkNode { Id = 2, Position = PointF.Empty };
        var data = new GraphData();
        data.nodeList.AddRange(new[] { a, b });

        GraphEditor.AddEdgeIfMissing(data, a, b, 3);
        GraphEditor.AddEdgeIfMissing(data, b, a, 3);

        Assert.Single(data.edgeList);
    }

    [Fact]
    public void CanPlaceNode_ReturnsFalseWhenNewNodeWouldOverlapExistingNode()
    {
        var data = new GraphData();
        data.nodeList.Add(new NetworkNode { Id = 1, Position = new PointF(50, 50), Radius = 10 });

        Assert.False(GraphEditor.CanPlaceNode(data, new PointF(55, 50), 10));
    }

    [Fact]
    public void AddNode_ReturnsNodeWithCorrectId()
    {
        var data = new GraphData();
        data.nodeList.Add(new NetworkNode { Id = 1, Position = PointF.Empty });

        var node = GraphEditor.AddNode(data, new PointF(100, 100));

        Assert.Equal(2, node.Id);
        Assert.Equal(2, data.nodeList.Count);
    }
}
