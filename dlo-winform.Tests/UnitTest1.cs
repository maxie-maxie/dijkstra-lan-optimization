using Xunit;
using dlo_winform;
using Graph;
using System.Drawing;

namespace dlo_winform.Tests;

public class GraphDataTests
{
    [Fact]
    public void GraphData_StartsWithEmptyNodeAndEdgeLists()
    {
        var graphData = new GraphData();

        Assert.Empty(graphData.nodeList);
        Assert.Empty(graphData.edgeList);
    }

    [Fact]
    public void GraphData_GetNodeById_ReturnsCorrectNode()
    {
        var node1 = new NetworkNode { Id = 1, Position = new PointF(0, 0) };
        var node2 = new NetworkNode { Id = 2, Position = new PointF(10, 10) };
        var graphData = new GraphData();
        graphData.nodeList.Add(node1);
        graphData.nodeList.Add(node2);

        var result = graphData.GetNodeById(2);

        Assert.Equal(node2, result);
    }

    [Fact]
    public void GraphData_GetNodeById_ReturnsNullForMissingId()
    {
        var graphData = new GraphData();
        graphData.nodeList.Add(new NetworkNode { Id = 1, Position = new PointF(0, 0) });

        var result = graphData.GetNodeById(999);

        Assert.Null(result);
    }
}
